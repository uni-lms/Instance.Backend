namespace Uni.Instance.Backend.Api.Course.Data;

public class BaseCourseDto {
  public Guid Id { get; set; }
  public required string Name { get; set; }
  public required string Abbreviation { get; set; }
  public int Semester { get; set; }
}