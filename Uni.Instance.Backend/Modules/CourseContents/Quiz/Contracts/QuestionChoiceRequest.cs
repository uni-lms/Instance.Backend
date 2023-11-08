using Uni.Backend.Data;


namespace Uni.Instance.Backend.Modules.CourseContents.Quiz.Contracts;

public class QuestionChoiceRequest : BaseModel {
  public Guid? QuestionId { get; set; }
}