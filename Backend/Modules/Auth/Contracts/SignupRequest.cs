namespace Backend.Modules.Auth.Contracts;

public class SignupRequest
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public Guid Role { get; set; }
}