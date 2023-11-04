using JetBrains.Annotations;


namespace Uni.Instance.Backend.Modules.Auth.Contracts;

public class WhoamiResponse {
  public required string Email { [UsedImplicitly] get; set; }
  public required string Role { [UsedImplicitly] get; set; }
  public required string FullName { get; set; }
}