using FastEndpoints;

using FluentValidation;

using Microsoft.AspNetCore.Identity.Data;


namespace Aip.Instance.Backend.Api.Auth.Validation;

public class LoginRequestValidator : Validator<LoginRequest> {
  public LoginRequestValidator() {
    RuleFor(e => e.Email).NotEmpty().EmailAddress().MaximumLength(50);
    RuleFor(e => e.Password).NotEmpty();
  }
}