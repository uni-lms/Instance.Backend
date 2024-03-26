namespace Aip.Instance.Backend.Api.Content.Text.Data;

public class UpdateTextContentRequest {
  public Guid Id { get; set; }
  public string Text { get; set; }
  public Guid SectionId { get; set; }
  public bool IsVisibleToInterns { get; set; }
}