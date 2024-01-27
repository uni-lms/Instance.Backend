using Ardalis.Result;

using FastEndpoints;

using JetBrains.Annotations;

using Uni.Instance.Backend.Api.Auth.Data;
using Uni.Instance.Backend.Api.Auth.Services;
using Uni.Instance.Backend.Configuration.Swagger;
using Uni.Instance.Backend.Extensions;


namespace Uni.Instance.Backend.Api.Auth.Endpoints.SignUp;

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