using FastEndpoints;

using JetBrains.Annotations;


namespace Aip.Instance.Backend.Api.Auth.Data;

public sealed class SignUpRequest {
  public required string Email { get; [UsedImplicitly] set; }
  public required string FirstName { get; [UsedImplicitly] set; }
  public required string LastName { get; [UsedImplicitly] set; }
  public string? Patronymic { get; [UsedImplicitly] set; }
  public required string Password { get; [UsedImplicitly] set; }

  [FromHeader("X-FCM-Token", IsRequired = false)]
  public string? FcmToken { get; set; }
}