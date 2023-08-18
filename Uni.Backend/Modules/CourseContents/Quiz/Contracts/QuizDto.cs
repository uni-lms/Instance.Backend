namespace Uni.Backend.Modules.CourseContents.Quiz.Contracts; 

public class QuizDto {
  public required string Title { get; set; }
  public string? Description { get; set; }
  public DateTime AvailableUntil { get; set; }
}