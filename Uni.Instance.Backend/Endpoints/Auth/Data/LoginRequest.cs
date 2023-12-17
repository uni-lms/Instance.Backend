namespace Uni.Instance.Backend.Endpoints.Auth.Data;

public class LoginRequest {
  public required string Email { get; set; }
  public required string Password { get; set; }
}