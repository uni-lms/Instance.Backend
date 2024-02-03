namespace Uni.Instance.Backend.Api.Course.Data;

public class StudentCourseDto : BaseCourseDto {
  public required List<string> Owners { get; set; }
}