using JetBrains.Annotations;


namespace Uni.Backend.Modules.Auth.Contracts;

public class RestorePasswordRequest {
  public required string Email { get; [UsedImplicitly] set; }
  public required string NewPassword { get; [UsedImplicitly] set; }
}