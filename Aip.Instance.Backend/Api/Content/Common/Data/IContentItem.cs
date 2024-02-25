namespace Aip.Instance.Backend.Api.Content.Common.Data;

public interface IContentItem {
  public Guid Id { get; set; }
  public string Type { get; }
}