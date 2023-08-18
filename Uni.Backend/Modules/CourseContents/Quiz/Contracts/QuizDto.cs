using JetBrains.Annotations;


namespace Uni.Backend.Modules.CourseContents.Quiz.Contracts;

public class QuizDto {
  public required string Title { [UsedImplicitly] get; set; }
  public string? Description { [UsedImplicitly] get; set; }
  public DateTime AvailableUntil { [UsedImplicitly] get; set; }
}