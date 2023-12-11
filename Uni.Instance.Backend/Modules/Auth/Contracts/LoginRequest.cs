using System.ComponentModel;


namespace Uni.Backend.Modules.Auth.Contracts;

public class LoginRequest {
  [DefaultValue("me@example.com")]
  public required string Email { get; init; }

  public required string Password { get; init; }
}