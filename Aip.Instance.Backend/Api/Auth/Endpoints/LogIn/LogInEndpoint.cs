using Aip.Instance.Backend.Api.Auth.Data;
using Aip.Instance.Backend.Api.Auth.Services;
using Aip.Instance.Backend.Configuration.Swagger;
using Aip.Instance.Backend.Extensions;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Auth.Endpoints.LogIn;

public class LogInEndpoint(AuthService service) : Endpoint<LogInRequest, LogInResponse> {
  public override void Configure() {
    Version(2);
    Post("/auth/log-in");
    Options(x => x.WithTags(ApiTags.Auth.Tag));
    AllowAnonymous();
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(LogInRequest req, CancellationToken ct) {
    var result = await service.LoginAsync(ValidationFailed, ValidationFailures, req, ct);

    await this.SendResponseAsync(result, ct);
  }
}