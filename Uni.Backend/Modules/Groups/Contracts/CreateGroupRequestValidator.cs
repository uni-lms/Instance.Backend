using FastEndpoints;

using FluentValidation;


namespace Uni.Backend.Modules.Groups.Contracts;

public class CreateGroupRequestValidator : Validator<CreateGroupRequest> {
  public CreateGroupRequestValidator() {
    RuleFor(e => e.Name)
      .NotEmpty()
      .WithMessage("Name of group is required");

    RuleFor(e => e.CurrentSemester)
      .NotEmpty()
      .WithMessage("Current semester must be presented and not equal to zero");

    RuleFor(e => e.MaxSemester)
      .NotEmpty()
      .WithMessage("Max semester must be presented and not equal to zero")
      .Must((model, field) => field >= model.CurrentSemester)
      .WithMessage("Max semester must be greater or equal than current semester");

    RuleFor(e => e.Users)
      .NotEmpty()
      .WithMessage("Group must not be without students");
  }
}