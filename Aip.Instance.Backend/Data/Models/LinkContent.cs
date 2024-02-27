using Aip.Instance.Backend.Data.Common;


namespace Aip.Instance.Backend.Data.Models;

public class LinkContent : BaseContent {
  public string Title { get; set; }
  public Uri Link { get; set; }
}