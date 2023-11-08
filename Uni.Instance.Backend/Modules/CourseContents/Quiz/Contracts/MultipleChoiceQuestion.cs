using Uni.Backend.Data;
using Uni.Backend.Modules.CourseContents.Quiz.Contracts;


namespace Uni.Instance.Backend.Modules.CourseContents.Quiz.Contracts;

public class MultipleChoiceQuestion : BaseModel {
  public required int SequenceNumber { get; set; }
  public required string Text { get; set; }
  public int MaximumPoints { get; set; }
  public bool IsMultipleChoicesAllowed { get; set; }
  public bool IsGivingPointsForIncompleteAnswersEnabled { get; set; }
  public required List<QuestionChoice> Choices { get; set; }
}