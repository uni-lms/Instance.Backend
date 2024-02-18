using Aip.Instance.Backend.Api.Auth.Data;
using Aip.Instance.Backend.Api.Auth.Services;
using Aip.Instance.Backend.Configuration.Swagger;
using Aip.Instance.Backend.Extensions;

using Ardalis.Result;

using FastEndpoints;

using JetBrains.Annotations;


namespace Aip.Instance.Backend.Api.Auth.Endpoints.SignUp;

[UsedImplicitly]
public class SignUpEndpoint(AuthService service) : Endpoint<SignUpRequest, Result<LogInResponse>> {
  public override void Configure() {
    Post("/auth/sign-up");
    Version(2);
    AllowAnonymous();
    Options(x => x.WithTags(ApiTags.Auth.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(SignUpRequest req, CancellationToken ct) {
    var result = await service.SignUpAsync(ValidationFailed, ValidationFailures, req, ct);

    await this.SendResponseAsync(result, cancellation: ct);
  }
}