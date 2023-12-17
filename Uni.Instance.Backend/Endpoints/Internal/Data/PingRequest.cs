using FastEndpoints;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc;


namespace Uni.Instance.Backend.Endpoints.Internal.Data;

public class PingRequest {
  [FromQuery]
  [BindFrom("success")]
  public bool Success { get; [UsedImplicitly] set; }
}