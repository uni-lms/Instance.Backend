namespace Aip.Instance.Backend.Api.Content.File.Data;

public class UploadFileContentRequest {
  public Guid CourseId { get; set; }
  public Guid SectionId { get; set; }
  public required IFormFile Content { get; set; }
  public required string Title { get; set; }
  public bool IsVisibleToStudents { get; set; }
}