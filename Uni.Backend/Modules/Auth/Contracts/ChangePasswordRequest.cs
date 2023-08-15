using JetBrains.Annotations;

namespace Uni.Backend.Modules.Auth.Contracts;

public class ChangePasswordRequest
{
    public required string OldPassword { get; [UsedImplicitly] set; }
    public required string NewPassword { get; [UsedImplicitly] set; }
}