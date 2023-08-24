using JetBrains.Annotations;


namespace Uni.Backend.Modules.CourseContents.Quiz.Contracts; 

public class UpdateQuizPassAttemptRequest {
  public required Guid PassAttempt { get; [UsedImplicitly] set; }
  public required Guid Question { get; [UsedImplicitly] set; }
  public required List<Guid> SelectedChoices { get; [UsedImplicitly] set; }
}