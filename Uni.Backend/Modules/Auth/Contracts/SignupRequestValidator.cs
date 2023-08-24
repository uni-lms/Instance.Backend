using FastEndpoints;

using FluentValidation;


namespace Uni.Backend.Modules.Auth.Contracts;

public class SignupRequestValidator : Validator<SignupRequest> {
  public SignupRequestValidator() {
    RuleFor(e => e.FirstName)
      .NotEmpty()
      .WithMessage("First name is required")
      .MinimumLength(3)
      .WithMessage("Length of first name must be greater or equal 3");

    RuleFor(e => e.LastName)
      .NotEmpty()
      .WithMessage("Last name is required")
      .MinimumLength(3)
      .WithMessage("Length of last name must be greater or equal 3");

    RuleFor(e => e.Patronymic)
      .MinimumLength(3)
      .WithMessage("Length of last name must be greater or equal 3");

    RuleFor(e => e.DateOfBirth)
      .Must(value => value <= DateOnly.FromDateTime(DateTime.Today))
      .WithMessage("Date of birth must be in the past");

    RuleFor(e => e.Email)
      .EmailAddress()
      .WithMessage("Email address field must be valid email address");

    RuleFor(e => e.Role)
      .NotEmpty()
      .WithMessage("Role id must not be empty");
  }
}