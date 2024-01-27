using System.Security.Claims;

using Microsoft.AspNetCore.Authentication;


namespace Uni.Instance.Backend.Api.Auth.Services;

public sealed class UserRoleHydrator(AuthService service) : IClaimsTransformation {
  public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal) {
    var email = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
    ArgumentNullException.ThrowIfNull(email);

    var roleOfUser = await service.GetRoleOfUser(email);

    if (roleOfUser.Length != 0) {
      principal.AddIdentity(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, roleOfUser) }));
    }

    return principal;
  }
}