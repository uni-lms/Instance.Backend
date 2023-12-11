namespace Uni.Instance.Backend.Modules.CourseContents.Quiz.Contracts; 

public class AttemptInfo {
  public Guid Id { get; set; }
  public DateTime StartedAt { get; set; }
  public DateTime? FinishedAt { get; set; }
  public int AccruedPoints { get; set; }
  public Guid QuizId { get; set; }
}