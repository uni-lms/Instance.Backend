namespace Uni.Backend.Modules.CourseContents.Quiz.Contracts; 

public class QuizPassAttemptDetails {
  public required List<AccruedPointDto> AccruedPoints { get; set; }
  public TimeSpan TimeSpent { get; set; }

}