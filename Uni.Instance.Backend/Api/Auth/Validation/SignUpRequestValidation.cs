using FastEndpoints;

using FluentValidation;

using Uni.Instance.Backend.Api.Auth.Data;


namespace Uni.Instance.Backend.Api.Auth.Validation;

public class SignUpRequestValidation : Validator<SignUpRequest> {
  public SignUpRequestValidation() {
    RuleFor(e => e.Email).NotEmpty().EmailAddress().MaximumLength(50);
    RuleFor(e => e.FirstName).NotEmpty().MaximumLength(30);
    RuleFor(e => e.LastName).NotEmpty().MaximumLength(30);
    RuleFor(e => e.Password).NotEmpty();
  }
}