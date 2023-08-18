using JetBrains.Annotations;


namespace Uni.Backend.Modules.CourseContents.Quiz.Contracts;

public class CreateQuizRequest {
  public Guid CourseId { get; [UsedImplicitly] set; }
  public Guid BlockId { get; [UsedImplicitly] set; }
  public required string Title { get; [UsedImplicitly] set; }
  public string? Description { get; [UsedImplicitly] set; }
  public TimeSpan TimeLimit { get; [UsedImplicitly] set; }
  public bool IsQuestionsShuffled { get; [UsedImplicitly] set; }
  public DateTime AvailableUntil { get; [UsedImplicitly] set; }
  public required List<Guid> Questions { get; [UsedImplicitly] set; }
}