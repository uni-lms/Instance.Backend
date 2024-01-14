using FastEndpoints;

using JetBrains.Annotations;

using Uni.Instance.Backend.Configuration.Swagger;
using Uni.Instance.Backend.Endpoints.Auth.Data;
using Uni.Instance.Backend.Endpoints.Auth.Services;
using Uni.Instance.Backend.Extensions;


namespace Uni.Instance.Backend.Endpoints.Auth.Endpoints.Signup;

[UsedImplicitly]
public class SignupEndpoint(AuthService service) : Endpoint<SignupRequest> {
  public override void Configure() {
    Post("/auth/sign-up");
    Version(2);
    AllowAnonymous();
    Options(x => x.WithTags(ApiTags.Auth.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(SignupRequest req, CancellationToken ct) {
    var result = await service.SignUpAsync(ValidationFailed, ValidationFailures, req, ct);

    await this.SendResponseAsync(result, cancellation: ct);
  }
}