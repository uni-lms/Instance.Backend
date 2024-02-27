namespace Aip.Instance.Backend.Api.Content.Link.Data;

public class UpdateLinkContentRequest {
  public Guid Id { get; set; }
  public Uri Link { get; set; }
  public Guid SectionId { get; set; }
  public bool IsVisibleToInterns { get; set; }
}