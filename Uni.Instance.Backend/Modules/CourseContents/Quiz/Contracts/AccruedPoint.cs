using Uni.Backend.Data;
using Uni.Backend.Modules.CourseContents.Quiz.Contracts;


namespace Uni.Instance.Backend.Modules.CourseContents.Quiz.Contracts;

public class AccruedPoint : BaseModel {
  public required MultipleChoiceQuestion Question { get; set; }
  public required List<QuestionChoice> SelectedChoices { get; set; } 
  public int AmountOfPoints { get; set; }
}