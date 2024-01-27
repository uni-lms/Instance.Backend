using JetBrains.Annotations;


namespace Uni.Instance.Backend.Api.Auth.Data;

public sealed class SignUpRequest {
  public required string Email { get; [UsedImplicitly] set; }
  public required string FirstName { get; [UsedImplicitly] set; }
  public required string LastName { get; [UsedImplicitly] set; }
  public string? Patronymic { get; [UsedImplicitly] set; }
  public required string Password { get; [UsedImplicitly] set; }
  public required Guid RoleId { get; [UsedImplicitly] set; }
}