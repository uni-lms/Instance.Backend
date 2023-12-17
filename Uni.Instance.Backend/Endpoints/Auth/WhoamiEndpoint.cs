using Ardalis.Result;

using FastEndpoints;

using Uni.Instance.Backend.Configuration;
using Uni.Instance.Backend.Data;
using Uni.Instance.Backend.Endpoints.Auth.Data;
using Uni.Instance.Backend.Endpoints.Auth.Services;
using Uni.Instance.Backend.Extensions;


namespace Uni.Instance.Backend.Endpoints.Auth;

public class WhoamiEndpoint(AppDbContext db, AuthService service) : EndpointWithoutRequest<Result<WhoamiResponse>> {
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