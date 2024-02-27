namespace Aip.Instance.Backend.Api.Content.Common.Data;

public class TextContentItem : IContentItem {
  public string Text { get; set; }
  public Guid Id { get; set; }
  public string Type => "text";
}