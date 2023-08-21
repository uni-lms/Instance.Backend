using FastEndpoints;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc;


namespace Uni.Backend.Modules.CourseContents.Quiz.Contracts;

public class UpdateQuizRequest {
  [FromRoute]
  [BindFrom("id")]
  public Guid Id { get; [UsedImplicitly] set; }
  public Guid BlockId { get; [UsedImplicitly] set; }
  public required string Title { get; [UsedImplicitly] set; }
  public string? Description { get; [UsedImplicitly] set; }
  public TimeSpan TimeLimit { get; [UsedImplicitly] set; }
  public bool IsQuestionsShuffled { get; [UsedImplicitly] set; }
  public DateTime AvailableUntil { get; [UsedImplicitly] set; }
  public required List<CreateQuizQuestionRequest> Questions { get; [UsedImplicitly] set; }
  public int AmountOfAllowedAttempts { get; set; }
}