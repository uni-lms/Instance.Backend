using FastEndpoints;

using Uni.Instance.Backend.Configuration.Swagger;
using Uni.Instance.Backend.Endpoints.Auth.Data;
using Uni.Instance.Backend.Endpoints.Auth.Services;
using Uni.Instance.Backend.Extensions;


namespace Uni.Instance.Backend.Endpoints.Auth.Endpoints.Login;

public class LogInEndpoint(AuthService service) : Endpoint<LogInRequest, LogInResponse> {
  public override void Configure() {
    Version(2);
    Post("/auth/log-in");
    Options(x => x.WithTags(ApiTags.Auth.Tag));
    AllowAnonymous();
  }

  public override async Task HandleAsync(LogInRequest req, CancellationToken ct) {
    var result = await service.LoginAsync(ValidationFailed, ValidationFailures, req, ct);

    await this.SendResponseAsync(result, ct);
  }
}