using Ardalis.Result;

using FluentValidation.Results;

using Microsoft.EntityFrameworkCore;

using Uni.Instance.Backend.Data;
using Uni.Instance.Backend.Data.Common;
using Uni.Instance.Backend.Data.Models;
using Uni.Instance.Backend.Endpoints.Groups.Data;
using Uni.Instance.Backend.Extensions;


namespace Uni.Instance.Backend.Endpoints.Groups.Services;

public class GroupsService(AppDbContext db) {
  public async Task<Result<GroupDto>> GetGroupByIdAsync(SearchByIdModel req, CancellationToken ct) {
    var group = await db.Groups.Where(e => e.Id == req.Id).FirstOrDefaultAsync(ct);

    if (group is null) {
      return Result.NotFound();
    }

    var dto = new GroupDto {
      Id = group.Id,
      Name = group.Name,
      EnteringYear = group.EnteringYear,
      GraduationYear = group.EnteringYear + group.YearsOfStudy,
    };

    return Result.Success(dto);
  }

  public async Task<Result<CreateGroupResponse>> CreateGroupAsync(
    bool validationFailed,
    IEnumerable<ValidationFailure> validationFailures,
    CreateGroupRequest req,
    CancellationToken ct
  ) {
    if (validationFailed) {
      return Result.Invalid(validationFailures.ToValidationErrors());
    }

    var group = new Group {
      Name = req.Name,
      EnteringYear = req.EnteringYear,
      YearsOfStudy = req.YearsOfStudy,
      Courses = [],
      Students = [],
    };

    await db.Groups.AddAsync(group, ct);
    await db.SaveChangesAsync(ct);

    return Result.Success(new CreateGroupResponse {
      Id = group.Id,
      Name = group.Name,
    });
  }

  public async Task<Result<EditGroupResponse>> EditGroupAsync(
    bool validationFailed,
    IEnumerable<ValidationFailure> validationFailures,
    EditGroupRequest req,
    CancellationToken ct
  ) {
    if (validationFailed) {
      return Result.Invalid(validationFailures.ToValidationErrors());
    }

    var group = await db.Groups.Where(e => e.Id == req.Id).FirstOrDefaultAsync(ct);

    if (group is null) {
      return Result.NotFound();
    }

    group.Name = req.Name;
    group.EnteringYear = req.EnteringYear;
    group.YearsOfStudy = req.YearsOfStudy;

    await db.SaveChangesAsync(ct);

    return Result.Success(new EditGroupResponse {
      Name = req.Name,
    });
  }

  public async Task<Result<EditGroupResponse>> DeleteGroupAsync(SearchByIdModel req, CancellationToken ct) {
    var group = await db.Groups.Where(e => e.Id == req.Id).FirstOrDefaultAsync(ct);

    if (group is null) {
      return Result.NotFound();
    }

    db.Groups.Remove(group);
    await db.SaveChangesAsync(ct);

    return Result.Success(new EditGroupResponse {
      Name = group.Name,
    });
  }
}