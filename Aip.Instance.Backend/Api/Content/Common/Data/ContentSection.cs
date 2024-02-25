namespace Aip.Instance.Backend.Api.Content.Common.Data;

public class ContentSection {
  public string Name { get; set; }
  public required IEnumerable<IContentItem> Items { get; set; }
}