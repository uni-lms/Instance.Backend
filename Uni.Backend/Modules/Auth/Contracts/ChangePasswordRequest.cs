namespace Uni.Backend.Modules.Auth.Contracts;

public class ChangePasswordRequest
{
    public required string OldPassword { get; set; }
    public required string NewPassword { get; set; }
}