namespace Uni.Instance.Backend.Api.Course.Data;

public class CourseOwnersResponse {
  public Guid CourseId { get; set; }
  public required string CourseName { get; set; }
  public required List<CourseOwner> Owners { get; set; }
}