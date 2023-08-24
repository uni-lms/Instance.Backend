using JetBrains.Annotations;


namespace Uni.Backend.Modules.Static.Contracts;

public class SearchFileRequest {
  public required string FileId { get; [UsedImplicitly] set; }
}