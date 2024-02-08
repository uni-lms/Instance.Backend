namespace Uni.Instance.Backend.Api.CourseContent.File.Data;

public class UploadFileContentRequest {
  public Guid CourseId { get; set; }
  public Guid SectionId { get; set; }
  public required IFormFile Content { get; set; }
  public required string Title { get; set; }
  public DateTime? AvailableSince { get; set; }
  public DateTime? AvailableUntil { get; set; }
}