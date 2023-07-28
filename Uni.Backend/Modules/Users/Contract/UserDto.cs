namespace Uni.Backend.Modules.Users.Contract;

public class UserDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? Patronymic { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? Email { get; set; }
    public string? RoleName { get; set; }
    public string? GenderName { get; set; }
}