using FastEndpoints;

using Riok.Mapperly.Abstractions;

using Uni.Backend.Modules.CourseContents.Quiz.Contracts;


namespace Uni.Instance.Backend.Modules.CourseContents.Quiz.Contracts;

[Mapper]
public partial class QuizMapper : ResponseMapper<QuizDto, QuizContent> {
  public override QuizDto FromEntity(QuizContent e) {
    var dto = new QuizDto {
      Title = e.Title,
      AvailableUntil = e.AvailableUntil,
      Description = e.Description,
      Id = e.Id,
      IsVisibleToStudents = e.IsVisibleToStudents,
    };

    return dto;
  }
}