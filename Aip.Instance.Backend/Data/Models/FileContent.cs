using Aip.Instance.Backend.Data.Common;


namespace Aip.Instance.Backend.Data.Models;

public class FileContent : BaseContent {
  public required string Title { get; set; }
  public required StaticFile File { get; set; }
}