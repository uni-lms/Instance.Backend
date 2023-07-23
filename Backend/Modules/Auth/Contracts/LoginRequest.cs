namespace Backend.Modules.Auth.Contracts;

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}