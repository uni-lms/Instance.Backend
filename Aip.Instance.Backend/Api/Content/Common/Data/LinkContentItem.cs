namespace Aip.Instance.Backend.Api.Content.Common.Data;

public class LinkContentItem : IContentItem {
  public required Uri Link { get; set; }
  public required string Title { get; set; }
  public Guid Id { get; set; }
  public string ContentType => "link";
}