using Ardalis.Result;

using FluentValidation.Results;

using Uni.Instance.Backend.Extensions;


namespace Uni.Instance.Backend.Endpoints.Internal.Services;

public class PingService {
  public Result<string> Ping(bool validationFailed, List<ValidationFailure> validationFailures) {
    if (validationFailed) {
      return Result.Invalid(validationFailures.ToValidationErrors());
    }

    return Result.Success("pong");
  }
}