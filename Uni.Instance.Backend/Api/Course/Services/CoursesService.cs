using System.Security.Claims;

using Ardalis.Result;

using FastEndpoints.Security;

using FluentValidation.Results;

using Microsoft.EntityFrameworkCore;

using Uni.Instance.Backend.Api.Course.Data;
using Uni.Instance.Backend.Data;
using Uni.Instance.Backend.Data.Models;
using Uni.Instance.Backend.Extensions;


namespace Uni.Instance.Backend.Api.Course.Services;

public class CoursesService(AppDbContext db) {
  public async Task<Result<BaseCourseDto>> CreateCourseAsync(
    bool validationFailed,
    IEnumerable<ValidationFailure> validationFailures,
    ClaimsPrincipal user,
    CreateCourseRequest req,
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

    List<Group> groups = [];

    foreach (var groupId in req.AssignedGroups) {
      var group = await db.Groups.Where(e => e.Id == groupId).FirstOrDefaultAsync(ct);

      if (group is null) {
        return Result.NotFound($"Group {groupId} was not found");
      }

      groups.Add(group);
    }

    List<User> owners = [userData];

    foreach (var ownerId in req.Owners) {
      var owner = await db.Users.Where(e => e.Id == ownerId).FirstOrDefaultAsync(ct);

      if (owner is null) {
        return Result.NotFound($"User {ownerId} was not found");
      }

      owners.Add(owner);
    }

    var course = new Backend.Data.Models.Course {
      Name = req.Name,
      Abbreviation = req.Abbreviation,
      AssignedGroups = groups,
      Owners = owners,
      Semester = req.Semester,
    };

    await db.Courses.AddAsync(course, ct);
    await db.SaveChangesAsync(ct);

    var courseDto = new BaseCourseDto {
      Name = course.Name,
      Abbreviation = course.Abbreviation,
      Semester = course.Semester,
    };
    return Result.Success(courseDto);
  }
}