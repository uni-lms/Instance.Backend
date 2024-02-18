using Aip.Instance.Backend.Api.Internships.Data;

using FastEndpoints;

using FluentValidation;


namespace Aip.Instance.Backend.Api.Internships.Validation;

public class CreateCourseRequestValidator : Validator<CreateInternshipRequest> {
  public CreateCourseRequestValidator() {
    RuleFor(e => e.Name).MaximumLength(50);
  }
}