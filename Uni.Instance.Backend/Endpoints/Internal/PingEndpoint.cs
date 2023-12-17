using FastEndpoints;

using JetBrains.Annotations;

using Uni.Instance.Backend.Endpoints.Internal.Data;


namespace Uni.Instance.Backend.Endpoints.Internal;

[UsedImplicitly]
public sealed class PingEndpoint : Endpoint<EmptyRequest, PingReponse> {
  private static readonly PingReponse PingReponse = new() { Value = "Pong" };

  public override void Configure() {
    Version(2);
    Get("/internal/ping");
    Options(x => x.WithTags("Internal"));
    AllowAnonymous();
  }

  public override async Task<PingReponse> ExecuteAsync(EmptyRequest req, CancellationToken ct) {
    return PingReponse;
  }
}