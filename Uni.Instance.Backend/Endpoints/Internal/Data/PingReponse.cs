using JetBrains.Annotations;


namespace Uni.Instance.Backend.Endpoints.Internal.Data;

public class PingReponse {
  public required string Value { [UsedImplicitly] get; set; }
}