namespace Aip.Instance.Backend.Api.Content.Text.Data;

public class CreateTextContentRequest {
  public required Guid InternshipId { get; set; }
  public required Guid SectionId { get; set; }
  public bool IsVisibleToStudents { get; set; }
  public required string Text { get; set; }
}