namespace Backend.Modules.Users.Contract;

public class CreateUserRequest
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Patronymic { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public required string Email { get; set; }
    public required Guid Gender { get; set; }
}