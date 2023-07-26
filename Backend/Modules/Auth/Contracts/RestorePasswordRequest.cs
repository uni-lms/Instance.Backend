namespace Backend.Modules.Auth.Contracts;

public class RestorePasswordRequest
{
    public required string Email { get; set; }
    public required string NewPassword { get; set; }
}