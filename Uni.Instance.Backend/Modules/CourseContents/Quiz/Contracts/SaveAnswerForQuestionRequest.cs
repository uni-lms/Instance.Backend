using Microsoft.AspNetCore.Mvc;

using Uni.Backend.Data;


namespace Uni.Instance.Backend.Modules.CourseContents.Quiz.Contracts;

public class SaveAnswerForQuestionRequest {
  [FromRoute]
  public Guid AttemptId { get; set; }

  public Guid QuestionId { get; set; }
  public required List<BaseModel> Choices { get; set; }
}