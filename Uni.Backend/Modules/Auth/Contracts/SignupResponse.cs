using JetBrains.Annotations;

namespace Uni.Backend.Modules.Auth.Contracts;

public class SignupResponse
{
    public required string FirstName { [UsedImplicitly] get; set; }
    public required string LastName { [UsedImplicitly] get; set; }
    public required string Email { [UsedImplicitly] get; set; }
    public required string Password { [UsedImplicitly] get; set; }
}