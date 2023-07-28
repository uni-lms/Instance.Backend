using Uni.Backend.Data;
using Uni.Backend.Modules.Courses.Contract;
using Uni.Backend.Modules.Genders.Contracts;
using Uni.Backend.Modules.Roles.Contract;
using Uni.Backend.Modules.Static.Contracts;

namespace Uni.Backend.Modules.Users.Contract;

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
    public IEnumerable<Course>? OwnedCourses { get; set; }
}