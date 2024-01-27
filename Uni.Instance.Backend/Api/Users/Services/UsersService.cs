using Ardalis.Result;

using FluentValidation.Results;

using Microsoft.EntityFrameworkCore;

using Uni.Instance.Backend.Api.Users.Data;
using Uni.Instance.Backend.Data;
using Uni.Instance.Backend.Extensions;


namespace Uni.Instance.Backend.Api.Users.Services;

public class UsersService(AppDbContext db) {
  public async Task<Result<EditUserResponse>> EditUserAsync(
    bool validationFailed,
    IEnumerable<ValidationFailure> validationFailures,
    EditUserRequest req,
    CancellationToken ct
  ) {
    if (validationFailed) {
      return Result.Invalid(validationFailures.ToValidationErrors());
    }

    var user = await db.Users.Where(e => e.Id == req.Id).FirstOrDefaultAsync(ct);

    if (user is null) {
      return Result.NotFound();
    }

    if (req.Email is not null) {
      user.Email = req.Email;
    }


    if (req.FirstName is not null) {
      user.FirstName = req.FirstName;
    }


    if (req.LastName is not null) {
      user.LastName = req.LastName;
    }

    user.Patronymic = req.Patronymic;

    await db.SaveChangesAsync(ct);

    return Result.Success(new EditUserResponse {
      Id = user.Id,
    });
  }
}