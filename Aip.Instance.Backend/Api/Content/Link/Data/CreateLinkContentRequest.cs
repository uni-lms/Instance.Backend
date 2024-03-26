namespace Aip.Instance.Backend.Api.Content.Link.Data;

public class CreateLinkContentRequest {
  public required Guid InternshipId { get; set; }
  public required Guid SectionId { get; set; }
  public bool IsVisibleToStudents { get; set; }
  public required Uri Link { get; set; }
  public string Title { get; set; }
}