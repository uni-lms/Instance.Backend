namespace Uni.Instance.Backend.Api.CourseContent.File.Data;

public class EditFileContentRequest {
  public Guid Id { get; set; }
  public string? Title { get; set; }
  public bool IsVisibleToStudents { get; set; }
}