namespace Uni.Instance.Backend.Modules.CourseContents.Text.Contract; 

public class GetTextContentInfoResponse {
  public required string CourseAbbreviation { get; set; }
  public required string VisibleName { get; set; }
}