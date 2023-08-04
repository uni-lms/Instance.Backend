namespace Uni.Backend.Modules.Users.Contracts;

public class UserCredentials
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string FirstName { get; set; }
    public required string Patronymic { get; set; }
}