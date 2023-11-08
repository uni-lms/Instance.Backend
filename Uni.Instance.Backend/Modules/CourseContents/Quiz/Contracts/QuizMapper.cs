using FastEndpoints;

using Riok.Mapperly.Abstractions;

using Uni.Instance.Backend.Modules.CourseContents.Quiz.Contracts;


namespace Uni.Backend.Modules.CourseContents.Quiz.Contracts;

[Mapper]
public partial class QuizMapper : ResponseMapper<QuizDto, QuizContent> {
  public partial QuizDto FromEntity(QuizContent e);
}