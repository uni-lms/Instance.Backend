namespace Aip.Instance.Backend.Api.Content.Assignment.Data;

public class UpdateAssignmentRequest {
  public Guid Id { get; set; }
  public Guid SectionId { get; set; }
  public DateTime Deadline { get; set; }
  public string Title { get; set; }
  public string? Description { get; set; }
  public IFormFile? File { get; set; }
  public bool IsVisibleToInterns { get; set; }
}