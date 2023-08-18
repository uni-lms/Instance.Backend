namespace Uni.Backend.Modules.Static.Contracts;

public class FileSaveResult {
  public bool IsSuccess { get; init; }
  public string? FilePath { get; init; }
  public string? FileId { get; init; }
}