using Uni.Backend.Data;
using Uni.Backend.Modules.Courses.Contracts;
using Uni.Backend.Modules.Users.Contracts;


namespace Uni.Backend.Modules.Groups.Contracts;

public class Group : BaseModel {
  public required string Name { get; set; }
  public int CurrentSemester { get; set; }
  public int MaxSemester { get; set; }
  public required List<User> Students { get; set; }
  public List<Course>? Courses { get; set; }
}