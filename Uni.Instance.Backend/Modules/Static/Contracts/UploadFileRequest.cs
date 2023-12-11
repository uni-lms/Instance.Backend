using JetBrains.Annotations;


namespace Uni.Backend.Modules.Static.Contracts;

public class UploadFileRequest {
  public required IFormFile File { get; [UsedImplicitly] set; }
  public required string VisibleName { get; [UsedImplicitly] set; }
}