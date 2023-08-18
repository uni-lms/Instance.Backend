using Uni.Backend.Data;
using Uni.Backend.Modules.Courses.Contracts;
using Uni.Backend.Modules.Genders.Contracts;
using Uni.Backend.Modules.Roles.Contracts;
using Uni.Backend.Modules.Static.Contracts;


namespace Uni.Backend.Modules.Users.Contracts;

public class User : BaseModel {
  public required string FirstName { get; set; }
  public required string LastName { get; set; }
  public string? Patronymic { get; set; }
  public Gender? Gender { get; set; }
  public DateOnly DateOfBirth { get; set; }
  public required string Email { get; set; }
  public required byte[] PasswordHash { get; set; }
  public required byte[] PasswordSalt { get; set; }
  public Role? Role { get; set; }
  public StaticFile? Avatar { get; set; }
  public List<Course>? OwnedCourses { get; set; }
}