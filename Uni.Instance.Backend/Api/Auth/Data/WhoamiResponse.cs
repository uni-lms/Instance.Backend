namespace Uni.Instance.Backend.Api.Auth.Data;

public class WhoamiResponse {
  public required string Email { get; set; }
  public required string RoleName { get; set; }
  public required string FullName { get; set; }
}