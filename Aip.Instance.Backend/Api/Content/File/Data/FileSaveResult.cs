namespace Aip.Instance.Backend.Api.Content.File.Data;

public class FileSaveResult {
  public bool IsSuccess { get; set; }
  public string? FileId { get; set; }
  public string? FilePath { get; set; }
  public string? Error { get; set; }
}