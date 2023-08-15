using System.ComponentModel;
using JetBrains.Annotations;

namespace Uni.Backend.Modules.Auth.Contracts;

public class SignupRequest
{
    public required string FirstName { get; [UsedImplicitly] init; }
    public required string LastName { get; [UsedImplicitly] init; }
    public required string? Patronymic { get; [UsedImplicitly] init; }
    public DateOnly DateOfBirth { get; [UsedImplicitly] init; }
    [DefaultValue("me@example.com")]
    public required string Email { get; [UsedImplicitly] init; }
    public Guid Role { get; [UsedImplicitly] init; }
    public Guid Gender { get; [UsedImplicitly] init; }
    public string? Avatar { get; [UsedImplicitly] init; }
}