using FastEndpoints;

using FluentValidation;

using Uni.Instance.Backend.Endpoints.Groups.Data;


namespace Uni.Instance.Backend.Endpoints.Groups.Validation;

public class CreateGroupRequestValidator : Validator<CreateGroupRequest> {
  public CreateGroupRequestValidator() {
    RuleFor(e => e.Name).NotEmpty();
    RuleFor(e => e.EnteringYear).GreaterThan(2000);
    RuleFor(e => e.YearsOfStudy).GreaterThan(1);
  }
}