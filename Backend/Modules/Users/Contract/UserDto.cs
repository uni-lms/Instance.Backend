namespace Backend.Modules.Users.Contract;

public class UserDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string? Patronymic { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public required string Email { get; set; }
    public required string RoleName { get; set; }
    public required string GenderName { get; set; }
}