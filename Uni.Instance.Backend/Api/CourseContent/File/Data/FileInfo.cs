namespace Uni.Instance.Backend.Api.CourseContent.File.Data;

public class FileInfo {
  public Guid Id { get; set; }
  public required string CourseAbbreviation { get; set; }
  public required string Title { get; set; }
  public required string FileSize { get; set; }
  public required string FileName { get; set; }
  public required string Extension { get; set; }
  public required string ContentType { get; set; }
}