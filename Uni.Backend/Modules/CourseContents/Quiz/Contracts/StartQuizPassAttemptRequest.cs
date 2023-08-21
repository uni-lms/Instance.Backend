using JetBrains.Annotations;


namespace Uni.Backend.Modules.CourseContents.Quiz.Contracts; 

public class StartQuizPassAttemptRequest {
  public required Guid Quiz { get; [UsedImplicitly] set; }
}