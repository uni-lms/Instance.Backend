using JetBrains.Annotations;


namespace Uni.Backend.Modules.Assignments.Contracts; 

public class CreateAssignmentRequestBody {
  public Guid Course { get; [UsedImplicitly] set; }
  public Guid Block { get; [UsedImplicitly] set; }
  public required string Title { get; [UsedImplicitly] set; }
  public string? Description { get; [UsedImplicitly] set; }
  public DateTime AvailableUntil { get; [UsedImplicitly] set; }
  public bool IsVisibleToStudents { get; [UsedImplicitly] set; }
  public int MaximumPoints { get; [UsedImplicitly] set; }
}