using System.Security.Claims;

using FastEndpoints;
using FastEndpoints.Security;

using Uni.Instance.Backend.Api.Auth.Data;
using Uni.Instance.Backend.Api.Auth.Services;
using Uni.Instance.Backend.Configuration.Swagger;
using Uni.Instance.Backend.Extensions;


namespace Uni.Instance.Backend.Api.Auth.Endpoints.Delete;

public class DeleteEndpoint(AuthService service) : Endpoint<EmptyRequest, WhoamiResponse> {
  public override void Configure() {
    Version(2);
    Delete("/auth/delete");
    Options(x => x.WithTags(ApiTags.Auth.Tag));
  }

  public override async Task HandleAsync(EmptyRequest req, CancellationToken ct) {
    var result = await service.DeleteAccountAsync(User.ClaimValue(ClaimTypes.Name)!);

    await this.SendResponseAsync(result, ct);
  }
}