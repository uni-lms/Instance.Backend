namespace Uni.Backend.Modules.CourseContents.Quiz.Contracts;

public class CreateQuizQuestionChoiceRequest {
  public required string Text { get; set; }
  public int AmountOfPoints { get; set; }
  public bool IsCorrect { get; set; }
}