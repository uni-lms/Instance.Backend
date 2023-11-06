namespace Uni.Instance.Backend.Modules.CourseContents.File.Contracts; 

public class GetFileContentInfoResponse {
  public required string CourseAbbreviation { get; set; }
  public required string VisibleName { get; set; }
  public required long FileSize { get; set; }
  public required string Extension { get; set; }
}