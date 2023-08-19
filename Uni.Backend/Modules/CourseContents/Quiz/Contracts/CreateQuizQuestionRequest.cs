namespace Uni.Backend.Modules.CourseContents.Quiz.Contracts; 

public class CreateQuizQuestionRequest {
  public required string Text { get; set; }
  public int MaximumPoints { get; set; }
  public bool IsMultipleChoicesAllowed { get; set; }
  public bool IsGivingPointsForIncompleteAnswersEnabled { get; set; }
  public required List<CreateQuizQuestionChoiceRequest> Choices { get; set; }
}