using JetBrains.Annotations;


namespace Uni.Backend.Modules.CourseContents.Quiz.Contracts; 

public class QuizPassAttemptDto {
  public Guid Id { [UsedImplicitly] get; set; }
  public int Points { [UsedImplicitly] get; set; }
}