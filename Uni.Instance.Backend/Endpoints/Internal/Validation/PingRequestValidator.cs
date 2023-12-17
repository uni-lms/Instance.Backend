using FastEndpoints;

using FluentValidation;

using Uni.Instance.Backend.Endpoints.Internal.Data;


namespace Uni.Instance.Backend.Endpoints.Internal.Validation;

public class PingRequestValidator : Validator<PingRequest> {
  public PingRequestValidator() {
    RuleFor(e => e.Success).Equal(true);
  }
}