namespace Aip.Instance.Backend.Api.Content.Common.Data;

public class LinkContentItem : IContentItem {
  public Uri Link { get; set; }
  public Guid Id { get; set; }
  public string Type => "link";
}