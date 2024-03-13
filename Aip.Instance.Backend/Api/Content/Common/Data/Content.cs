namespace Aip.Instance.Backend.Api.Content.Common.Data;

public class Content {
  public required string Title { get; set; }
  public required ICollection<ContentSection> Sections { get; set; }
}