using Uni.Backend.Data;
using Uni.Backend.Modules.Users.Contracts;


namespace Uni.Backend.Modules.CourseContents.Quiz.Contracts;

public class QuizPassAttempt : BaseModel {
  public required User User { get; set; }
  public required QuizContent Quiz { get; set; }
  public required List<AccruedPoint> AccruedPoints { get; set; }
  public DateTime StartedAt { get; set; }
  public DateTime? FinishedAt { get; set; }
}