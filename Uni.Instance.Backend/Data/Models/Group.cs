using Uni.Instance.Backend.Data.Common;


namespace Uni.Instance.Backend.Data.Models;

public class Group : BaseModel {
  public required string Name { get; set; }
  public int EnteringYear { get; set; }
  public int YearsOfStudy { get; set; }
  public required List<User> Students { get; set; }
  public required List<Course> Courses { get; set; }
}