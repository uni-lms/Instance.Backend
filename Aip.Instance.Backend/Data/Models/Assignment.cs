using Aip.Instance.Backend.Data.Common;


namespace Aip.Instance.Backend.Data.Models;

public class Assignment : BaseContent {
  public string Title { get; set; }
  public DateTime Deadline { get; set; }
  public StaticFile? File { get; set; }
  public string? Description { get; set; }
}