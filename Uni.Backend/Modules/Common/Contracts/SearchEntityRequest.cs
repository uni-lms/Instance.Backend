using JetBrains.Annotations;


namespace Uni.Backend.Modules.Common.Contracts;

public class SearchEntityRequest {
  public Guid Id { get; [UsedImplicitly] set; }
}