namespace Aip.Instance.Backend.Api.Content.Common.Data;

public class FileContentItem : IContentItem {
  public required string Title { get; set; }
  public Guid Id { get; set; }
  public string Type => "file";
}