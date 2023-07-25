namespace Backend.Modules.Auth.Contracts;

public class SignupRequest
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string? Patronymic { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public required string Email { get; set; }
    public Guid Role { get; set; }
    public Guid Gender { get; set; }
    public string? Avatar { get; set; }
}