namespace Uni.Instance.Backend.Api.Course.Data;

public class BaseCourseDto {
  public required string Name { get; set; }
  public required string Abbreviation { get; set; }
  public int Semester { get; set; }
}