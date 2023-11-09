using Uni.Backend.Data;
using Uni.Instance.Backend.Modules.CourseContents.Quiz.Contracts;


namespace Uni.Backend.Modules.CourseContents.Quiz.Contracts;

public class QuestionChoice : BaseModel {
  public required string Text { get; set; }
  public int AmountOfPoints { get; set; }
  public bool IsCorrect { get; set; }
  public List<AccruedPoint> AccruedPoints { get; set; }
}