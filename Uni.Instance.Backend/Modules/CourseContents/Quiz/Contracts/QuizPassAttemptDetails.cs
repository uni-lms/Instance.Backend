using JetBrains.Annotations;


namespace Uni.Backend.Modules.CourseContents.Quiz.Contracts; 

public class QuizPassAttemptDetails {
  public required List<AccruedPointDto> AccruedPoints { [UsedImplicitly] get; set; }
  public TimeSpan? TimeSpent { [UsedImplicitly] get; set; }

}