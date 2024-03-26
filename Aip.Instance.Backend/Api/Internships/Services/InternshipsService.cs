﻿using System.Security.Claims;

using Aip.Instance.Backend.Api.Auth.Data;
using Aip.Instance.Backend.Api.Auth.Services;
using Aip.Instance.Backend.Api.Common.Data;
using Aip.Instance.Backend.Api.Internships.Data;
using Aip.Instance.Backend.Data;
using Aip.Instance.Backend.Data.Common;
using Aip.Instance.Backend.Data.Models;
using Aip.Instance.Backend.Extensions;

using Ardalis.Result;

using FastEndpoints.Security;

using FluentValidation.Results;

using Microsoft.EntityFrameworkCore;

using NuGet.Packaging;

using Sqids;


namespace Aip.Instance.Backend.Api.Internships.Services;

public class InternshipsService(AppDbContext db, AuthService authService) {
  public async Task<Result<InternshipDto>> CreateInternship(
    bool validationFailed,
    IEnumerable<ValidationFailure> validationFailures,
    ClaimsPrincipal user,
    CreateInternshipRequest req,
    CancellationToken ct
  ) {
    if (validationFailed) {
      return Result.Invalid(validationFailures.ToValidationErrors());
    }

    var userEmail = user.ClaimValue(ClaimTypes.Name);

    if (userEmail is null) {
      return Result.Unauthorized();
    }

    var userData = await db.Users.Where(e => e.Email == userEmail).FirstOrDefaultAsync(ct);

    if (userData is null) {
      return Result.NotFound();
    }

    var course = new Internship {
      Name = req.Name,
    };

    var primaryTutorRole = await db.Roles.FirstAsync(e => e.Name == "PrimaryTutor", ct);

    await db.Internships.AddAsync(course, ct);
    await db.InternshipBasedRoles.AddAsync(new InternshipUserRole {
      Internship = course,
      Role = primaryTutorRole,
      User = userData,
    }, ct);
    await db.SaveChangesAsync(ct);

    var courseDto = new InternshipDto {
      Id = course.Id,
      Name = course.Name,
    };
    return Result.Success(courseDto);
  }

  public async Task<Result<List<InternshipDto>>> GetAllInternships(CancellationToken ct) {
    var courses = await db.Internships.Select(e => new InternshipDto {
      Id = e.Id,
      Name = e.Name,
    }).ToListAsync(ct);

    return Result.Success(courses);
  }

  public async Task<Result<InternshipDto>> DeleteInternship(SearchByIdModel req, CancellationToken ct) {
    var course = await db.Internships.Where(e => e.Id == req.Id).FirstOrDefaultAsync(ct);

    if (course is null) {
      return Result.NotFound();
    }

    db.Internships.Remove(course);
    await db.SaveChangesAsync(ct);

    return Result.Success(new InternshipDto {
      Id = course.Id,
      Name = course.Name,
    });
  }

  public async Task<Result<List<InternshipDto>>> GetOwnedInternships(ClaimsPrincipal user, CancellationToken ct) {
    var userEmail = user.ClaimValue(ClaimTypes.Name);

    if (userEmail is null) {
      return Result.Unauthorized();
    }

    var userData = await db.Users.Where(e => e.Email == userEmail).FirstOrDefaultAsync(ct);

    if (userData is null) {
      return Result.NotFound();
    }

    var primaryTutorRole = await db.Roles.FirstAsync(e => e.Name == "PrimaryTutor", ct);
    var invitedUserRole = await db.Roles.FirstAsync(e => e.Name == "InvitedTutor", ct);

    var courses = await db.InternshipBasedRoles
      .Include(e => e.User)
      .Include(e => e.Role)
      .Where(e =>
        e.User == userData && (e.Role == primaryTutorRole || e.Role == invitedUserRole))
      .Select(e => new InternshipDto {
        Id = e.Internship.Id,
        Name = e.Internship.Name,
      })
      .ToListAsync(ct);

    return Result.Success(courses);
  }

  public async Task<Result<List<InternshipDto>>> GetEnrolledInternships(
    ClaimsPrincipal user,
    CancellationToken ct
  ) {
    var userEmail = user.ClaimValue(ClaimTypes.Name);

    if (userEmail is null) {
      return Result.Unauthorized();
    }

    var userData = await db.Users.Where(e => e.Email == userEmail).FirstOrDefaultAsync(ct);

    if (userData is null) {
      return Result.NotFound(nameof(userEmail));
    }

    var internships = await db.InternshipBasedRoles
      .Include(e => e.Internship)
      .Include(e => e.Role)
      .Where(e => e.User == userData && e.Role.Name == "Intern")
      .Select(e => new InternshipDto {
        Id = e.Internship.Id,
        Name = e.Internship.Name,
      })
      .ToListAsync(ct);

    return Result.Success(internships);
  }

  public async Task<Result<InternshipDto>> InviteTutor(ListOfGuidsRequest req, CancellationToken ct) {
    var usersToAdd = db.Users.Where(e => req.Ids.Contains(e.Id)).AsEnumerable();

    var invitedTutorRole = await db.Roles.FirstAsync(e => e.Name == "InvitedTutor", ct);

    var internship = await db.Internships
      .Where(e => e.Id == req.EntityId)
      .Include(e => e.InternshipUserRoles)
      .FirstOrDefaultAsync(ct);

    if (internship is null) {
      return Result.NotFound();
    }

    internship.InternshipUserRoles.AddRange(usersToAdd.Select(e => new InternshipUserRole {
      Internship = internship,
      Role = invitedTutorRole,
      User = e,
    }));

    await db.SaveChangesAsync(ct);

    return Result.Success(new InternshipDto {
      Id = internship.Id,
      Name = internship.Name,
    });
  }

  public async Task<Result<SearchByIdModel>> InviteIntern(InviteInternRequest req, CancellationToken ct) {
    var internship = await db.Internships.Where(e => e.Id == req.Id).FirstOrDefaultAsync(ct);

    if (internship is null) {
      return Result.NotFound(nameof(internship));
    }

    var password = new SqidsEncoder<int>(new SqidsOptions {
      MinLength = 10,
    }).Encode(Guid.NewGuid().GetHashCode());

    await authService.SignUpAsync(false, Enumerable.Empty<ValidationFailure>(), new SignUpRequest {
      Email = req.Email,
      FirstName = req.FirstName,
      LastName = req.LastName,
      Password = password,
      Patronymic = req.Patronymic,
      FcmToken = null,
    }, ct);

    var user = await db.Users.Where(e => e.Email == req.Email).FirstOrDefaultAsync(ct);

    if (user is null) {
      return Result.NotFound(nameof(user));
    }

    var role = await db.Roles.Where(e => e.Name == "Intern").FirstAsync(ct);

    var userRole = new InternshipUserRole {
      User = user,
      Internship = internship,
      Role = role,
    };

    await db.InternshipBasedRoles.AddAsync(userRole, ct);

    return Result.Success();
  }
}