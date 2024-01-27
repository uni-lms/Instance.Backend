using FastEndpoints;

using FluentValidation;

using Uni.Instance.Backend.Api.Course.Data;


namespace Uni.Instance.Backend.Api.Course.Validation;

public class CreateCourseRequestValidator : Validator<CreateCourseRequest> {
  public CreateCourseRequestValidator() {
    RuleFor(e => e.Name).MaximumLength(50);
    RuleFor(e => e.Abbreviation).MaximumLength(10);
    RuleFor(e => e.Semester).NotEmpty().GreaterThan(0);
  }
}