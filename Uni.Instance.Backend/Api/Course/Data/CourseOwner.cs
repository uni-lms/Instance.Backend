namespace Uni.Instance.Backend.Api.Course.Data;

public class CourseOwner {
  public Guid Id { get; set; }
  public required string FirstName { get; set; }
  public required string LastName { get; set; }
  public string? Patronymic { get; set; }
}