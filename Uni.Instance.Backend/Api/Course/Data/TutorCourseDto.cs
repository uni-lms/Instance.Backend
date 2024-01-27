namespace Uni.Instance.Backend.Api.Course.Data;

public class TutorCourseDto : BaseCourseDto {
  public required List<string> AssignedGroups { get; set; }
}