using JetBrains.Annotations;

namespace Uni.Backend.Modules.Users.Contracts;

public class UserCredentials
{
    public required string Email { get; init; }
    public required string Password { [UsedImplicitly] get; set; }
    public required string FirstName { [UsedImplicitly] get; set; }
    public required string Patronymic { [UsedImplicitly] get; set; }
}