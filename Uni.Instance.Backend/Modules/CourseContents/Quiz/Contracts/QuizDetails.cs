namespace Uni.Instance.Backend.Modules.CourseContents.Quiz.Contracts;

public class QuizDetails {
  public required string VisibleName { get; set; }
  public required int AmountOfQuestions { get; set; }
  public required int AmountOfAttempts { get; set; }
  public required int RemainingAttempts { get; set; }
  public required int? TimeLimit { get; set; }
  public required List<AttemptInfo> Attempts { get; set; }
  public required bool HasStartedAttempt { get; set; }
  public required int MaximumPoints { get; set; }
}