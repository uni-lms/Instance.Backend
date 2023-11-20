using JetBrains.Annotations;

using Uni.Backend.Modules.SolutionChecks.Contracts;


namespace Uni.Backend.Modules.Assignments.Contracts;

public class AssignmentDto {
  public Guid Id { [UsedImplicitly] get; set; }
  public required string Title { [UsedImplicitly] get; set; }
  public string? Description { [UsedImplicitly] get; set; }
  public DateTime AvailableUntil { [UsedImplicitly] get; set; }
  public SolutionCheckStatus Status { get; set; }
  public required int MaximumPoints { [UsedImplicitly] get; set; }
  public required int Rating { [UsedImplicitly] get; set; }
}