using Ardalis.Result;

using FluentValidation.Results;

using Uni.Instance.Backend.Data;
using Uni.Instance.Backend.Data.Models;
using Uni.Instance.Backend.Endpoints.Groups.Data;
using Uni.Instance.Backend.Extensions;


namespace Uni.Instance.Backend.Endpoints.Groups.Services;

public class GroupsService(AppDbContext db) {
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
}