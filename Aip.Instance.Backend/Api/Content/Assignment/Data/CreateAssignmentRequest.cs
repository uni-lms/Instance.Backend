namespace Aip.Instance.Backend.Api.Content.Assignment.Data;

public class CreateAssignmentRequest {
  public required string Title { get; set; }
  public string? Description { get; set; }
  public Guid InternshipId { get; set; }
  public Guid SectionId { get; set; }
  public DateTime Deadline { get; set; }
  public bool IsVisibleToInterns { get; set; }
  public IFormFile? File { get; set; }
}