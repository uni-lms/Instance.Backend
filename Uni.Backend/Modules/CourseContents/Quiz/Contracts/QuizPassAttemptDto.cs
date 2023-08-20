namespace Uni.Backend.Modules.CourseContents.Quiz.Contracts; 

public class QuizPassAttemptDto {
  public Guid Id { get; set; }
  public int Points { get; set; }
  public TimeSpan TimeSpent { get; set; }
}