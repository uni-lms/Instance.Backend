using Microsoft.AspNetCore.Mvc;


namespace Uni.Instance.Backend.Modules.CourseContents.Quiz.Contracts;

public class SaveAnswerForQuestionRequest {
  [FromRoute]
  public Guid AttemptId { get; set; }

  public Guid QuestionId { get; set; }
  public required List<QuestionChoiceDto> Choices { get; set; }
}