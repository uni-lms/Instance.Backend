namespace Uni.Instance.Backend.Api.Course.Data;

public class CreateCourseRequest {
  public required string Name { get; set; }
  public required string Abbreviation { get; set; }
  public int Semester { get; set; }
  public required List<Guid> AssignedGroups { get; set; }
  public required List<Guid> Owners { get; set; }
}