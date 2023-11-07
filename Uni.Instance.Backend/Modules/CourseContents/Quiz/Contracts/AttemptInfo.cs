namespace Uni.Instance.Backend.Modules.CourseContents.Quiz.Contracts; 

public class AttemptInfo {
  public DateTime StartedAt { get; set; }
  public DateTime? FinishedAt { get; set; }
  public int AccruedPoints { get; set; }
}