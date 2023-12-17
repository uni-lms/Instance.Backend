using Ardalis.Result;

using FastEndpoints;

using JetBrains.Annotations;

using Uni.Instance.Backend.Endpoints.Internal.Data;
using Uni.Instance.Backend.Endpoints.Internal.Services;
using Uni.Instance.Backend.Extensions;


namespace Uni.Instance.Backend.Endpoints.Internal;

[UsedImplicitly]
public sealed class PingEndpoint(PingService service) : Endpoint<PingRequest, Result<string>> {
  public override void Configure() {
    Version(2);
    Get("/internal/ping");
    Options(x => x.WithTags("Internal"));
    AllowAnonymous();
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(PingRequest req, CancellationToken ct) {
    var result = service.Ping(ValidationFailed, ValidationFailures);

    await this.SendResponseAsync(result, cancellation: ct);
  }
}