namespace Uni.Instance.Backend.Api.CourseContent.File.Data;

public class FileSaveResult {
  public bool IsSuccess { get; set; }
  public string? FileId { get; set; }
  public string? FilePath { get; set; }
  public string? Error { get; set; }
}