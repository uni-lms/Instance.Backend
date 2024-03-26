using FastEndpoints;


namespace Aip.Instance.Backend.Api.Auth.Data;

public class LogInRequest {
  public required string Email { get; set; }
  public required string Password { get; set; }

  [FromHeader("X-FCM-Token", IsRequired = false)]
  public string? FcmToken { get; set; }
}