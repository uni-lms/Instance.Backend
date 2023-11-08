using JetBrains.Annotations;


namespace Uni.Backend.Modules.CourseContents.Quiz.Contracts; 

public class CreateQuizQuestionRequest {
  public required string Text { get; [UsedImplicitly] set; }
  public int MaximumPoints { get; [UsedImplicitly] set; }
  public bool IsMultipleChoicesAllowed { get; [UsedImplicitly] set; }
  public bool IsGivingPointsForIncompleteAnswersEnabled { get; [UsedImplicitly] set; }
  public required List<CreateQuizQuestionChoiceRequest> Choices { get; [UsedImplicitly] set; }
}