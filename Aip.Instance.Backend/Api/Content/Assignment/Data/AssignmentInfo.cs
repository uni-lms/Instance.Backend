namespace Aip.Instance.Backend.Api.Content.Assignment.Data;

public class AssignmentInfo {
  public Guid Id { get; set; }
  public required string Title { get; set; }
  public string? Description { get; set; }
  public DateTime Deadline { get; set; }
  public string? FileId { get; set; }
}