using Uni.Backend.Data;


namespace Uni.Instance.Backend.Modules.CourseContents.Quiz.Contracts;

public class QuestionChoiceDto : BaseModel {
  public required Guid QuestionId { get; set; }
  public required string Title { get; set; }
}