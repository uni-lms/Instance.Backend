using FastEndpoints;

using Uni.Instance.Backend.Configuration.Swagger;
using Uni.Instance.Backend.Endpoints.Auth.Data;
using Uni.Instance.Backend.Endpoints.Auth.Services;
using Uni.Instance.Backend.Extensions;


namespace Uni.Instance.Backend.Endpoints.Auth.Endpoints.Login;

public class LoginEndpoint(AuthService service) : Endpoint<LoginRequest, LoginResponse> {
  public override void Configure() {
    Version(2);
    Post("/auth/login");
    Options(x => x.WithTags(ApiTags.Auth.Tag));
    AllowAnonymous();
  }

  public override async Task HandleAsync(LoginRequest req, CancellationToken ct) {
    var result = await service.LoginAsync(ValidationFailed, ValidationFailures, req, ct);

    await this.SendResponseAsync(result, ct);
  }
}