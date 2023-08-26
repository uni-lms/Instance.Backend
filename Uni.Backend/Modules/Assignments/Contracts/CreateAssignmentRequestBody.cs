namespace Uni.Backend.Modules.Assignments.Contracts; 

public class CreateAssignmentRequestBody {
  public Guid Course { get; set; }
  public Guid Block { get; set; }
  public required string Title { get; set; }
  public string? Description { get; set; }
  public DateTime AvailableUntil { get; set; }
  public bool IsVisibleToStudents { get; set; }
  public int MaximumPoints { get; set; }
}