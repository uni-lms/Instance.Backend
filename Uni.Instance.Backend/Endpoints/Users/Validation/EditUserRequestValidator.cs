using FastEndpoints;

using FluentValidation;

using Uni.Instance.Backend.Endpoints.Users.Data;


namespace Uni.Instance.Backend.Endpoints.Users.Validation;

public class EditUserRequestValidator : Validator<EditUserRequest> {
  public EditUserRequestValidator() {
    RuleFor(e => e.Email).EmailAddress().MaximumLength(50);
    RuleFor(e => e.FirstName).MaximumLength(30);
    RuleFor(e => e.LastName).MaximumLength(30);
    RuleFor(e => e.Patronymic).MaximumLength(30);
  }
}