using FastEndpoints;

using FluentValidation;

using Uni.Instance.Backend.Api.Groups.Data;


namespace Uni.Instance.Backend.Api.Groups.Validation;

public class GroupValidator : Validator<IGroupRequest> {
  public GroupValidator() {
    RuleFor(e => e.Name).NotEmpty();
    RuleFor(e => e.EnteringYear).GreaterThan(2000);
    RuleFor(e => e.YearsOfStudy).GreaterThan(1);
  }
}