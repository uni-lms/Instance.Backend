using Aip.Instance.Backend.Data.Common;


namespace Aip.Instance.Backend.Data.Models;

public class Assignment : BaseContent {
  public string Title { get; set; }
  public DateTime Deadline { get; set; }
}