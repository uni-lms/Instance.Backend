using JetBrains.Annotations;


namespace Uni.Backend.Modules.Assignments.Contracts;

public class AssignmentDto {
  public Guid Id { [UsedImplicitly] get; set; }
  public required string Title { [UsedImplicitly] get; set; }
  public string? Description { [UsedImplicitly] get; set; }
  public DateTime AvailableUntil { [UsedImplicitly] get; set; }
}