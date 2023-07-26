using Backend.Data;
using Backend.Modules.Courses.Contract;
using Backend.Modules.Genders.Contracts;
using Backend.Modules.Roles.Contract;
using Backend.Modules.Static.Contracts;

namespace Backend.Modules.Users.Contract;

public class User : BaseModel
{
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
    public IEnumerable<Course> OwnedCourses { get; set; }
}