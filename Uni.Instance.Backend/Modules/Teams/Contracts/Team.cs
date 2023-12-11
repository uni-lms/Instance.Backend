using Uni.Backend.Data;
using Uni.Backend.Modules.Courses.Contracts;
using Uni.Backend.Modules.Users.Contracts;


namespace Uni.Backend.Modules.Teams.Contracts; 

public class Team: BaseModel {
  public string? Name { get; set; }
  public required List<User> Members { get; set; }
  public required Course Course { get; set; }
}