namespace Aip.Instance.Backend.Api.Content.Common.Data;

public class AssignmentContentItem : IContentItem {
  public string Title { get; set; }
  public Guid Id { get; set; }
  public string ContentType => "assignment";
}