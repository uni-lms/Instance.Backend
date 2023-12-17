using FastEndpoints;

using FluentResults;

using JetBrains.Annotations;


namespace Uni.Instance.Backend.Endpoints.Internal;

[UsedImplicitly]
public sealed class PingEndpoint : Endpoint<EmptyRequest, Result<string>> {
  public override void Configure() {
    Version(2);
    Get("/internal/ping");
    Options(x => x.WithTags("Internal"));
    AllowAnonymous();
  }

  public override async Task<Result<string>> ExecuteAsync(EmptyRequest req, CancellationToken ct) {
    return Result.Ok("pong");
  }
}