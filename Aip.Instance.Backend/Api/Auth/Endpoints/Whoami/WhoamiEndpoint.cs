using Aip.Instance.Backend.Api.Auth.Data;
using Aip.Instance.Backend.Api.Auth.Services;
using Aip.Instance.Backend.Configuration.Swagger;
using Aip.Instance.Backend.Extensions;

using Ardalis.Result;

using FastEndpoints;

using JetBrains.Annotations;


namespace Aip.Instance.Backend.Api.Auth.Endpoints.Whoami;

[UsedImplicitly]
public class WhoamiEndpoint(AuthService service) : EndpointWithoutRequest<Result<WhoamiResponse>> {
  public override void Configure() {
    Version(2);
    Get("/auth/whoami");
    Options(x => x.WithTags(ApiTags.Auth.Tag));
  }

  public override async Task HandleAsync(CancellationToken ct) {
    var result = await service.WhoamiAsync(User);

    await this.SendResponseAsync(result, ct);
  }
}