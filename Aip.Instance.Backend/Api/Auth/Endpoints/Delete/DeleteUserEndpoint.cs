using System.Security.Claims;

using Aip.Instance.Backend.Api.Auth.Data;
using Aip.Instance.Backend.Api.Auth.Services;
using Aip.Instance.Backend.Configuration.Swagger;
using Aip.Instance.Backend.Extensions;

using FastEndpoints;
using FastEndpoints.Security;


namespace Aip.Instance.Backend.Api.Auth.Endpoints.Delete;

public class DeleteUserEndpoint(AuthService service) : Endpoint<EmptyRequest, WhoamiResponse> {
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