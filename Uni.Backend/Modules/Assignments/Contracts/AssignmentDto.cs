namespace Uni.Backend.Modules.Assignments.Contracts;

public class AssignmentDto {
  public Guid Id { get; set; }
  public required string BlockName { get; set; }
  public required string Title { get; set; }
  public string? Description { get; set; }
  public DateTime AvailableUntil { get; set; }
  public bool IsVisibleToStudents { get; set; }
  public int MaximumPoints { get; set; }
}