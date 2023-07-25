using Backend.Data;
using Backend.Modules.Genders.Contracts;
using Backend.Modules.Roles.Contract;

namespace Backend.Modules.Users.Contract;

public class User : BaseModel
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? Patronymic { get; set; }
    public Gender Gender { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public required string Email { get; set; }
    public required byte[] PasswordHash { get; set; }
    public required byte[] PasswordSalt { get; set; }
    public required Role Role { get; set; }
}