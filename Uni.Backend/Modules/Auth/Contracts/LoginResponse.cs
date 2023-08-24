using JetBrains.Annotations;


namespace Uni.Backend.Modules.Auth.Contracts;

public class LoginResponse {
  public required string Email { [UsedImplicitly] get; set; }
  public required string Token { [UsedImplicitly] get; set; }
}