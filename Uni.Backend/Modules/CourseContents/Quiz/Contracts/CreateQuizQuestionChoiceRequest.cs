using JetBrains.Annotations;


namespace Uni.Backend.Modules.CourseContents.Quiz.Contracts;

public class CreateQuizQuestionChoiceRequest {
  public required string Text { get; [UsedImplicitly] set; }
  public int AmountOfPoints { get; [UsedImplicitly] set; }
  public bool IsCorrect { get; [UsedImplicitly] set; }
}