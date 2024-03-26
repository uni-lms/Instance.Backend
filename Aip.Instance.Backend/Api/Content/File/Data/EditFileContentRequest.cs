namespace Aip.Instance.Backend.Api.Content.File.Data;

public class EditFileContentRequest {
  public Guid Id { get; set; }
  public string? Title { get; set; }
  public bool IsVisibleToInterns { get; set; }
}