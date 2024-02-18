using Ardalis.Result;

using FastEndpoints;

using FluentValidation.Results;

using IResult = Ardalis.Result.IResult;


namespace Aip.Instance.Backend.Extensions;

public static class ArdalisExtensions {
  public static List<ValidationError> ToValidationErrors(this IEnumerable<ValidationFailure> failures) {
    return failures.Select(e => new ValidationError {
      Identifier = e.PropertyName,
      ErrorMessage = e.ErrorMessage,
    }).ToList();
  }

  public static async Task SendResponseAsync<TResult>(
    this IEndpoint ep,
    TResult result,
    CancellationToken cancellation = default
  ) where TResult : IResult {
    var statusCode = 200;
    switch (result.Status) {
      case ResultStatus.Ok: {
        break;
      }
      case ResultStatus.Error: {
        statusCode = 500;
        break;
      }
      case ResultStatus.Forbidden: {
        statusCode = 403;
        break;
      }
      case ResultStatus.Unauthorized: {
        statusCode = 401;
        break;
      }
      case ResultStatus.Invalid: {
        statusCode = 400;
        break;
      }
      case ResultStatus.NotFound: {
        statusCode = 404;
        break;
      }
      case ResultStatus.Conflict: {
        statusCode = 409;
        break;
      }
      case ResultStatus.CriticalError: {
        statusCode = 521;
        break;
      }
      case ResultStatus.Unavailable: {
        statusCode = 503;
        break;
      }
      default:
        throw new ArgumentOutOfRangeException(nameof(result), "Unknown status");
    }

    await ep.HttpContext.Response.SendAsync(result, statusCode, cancellation: cancellation);
  }
}