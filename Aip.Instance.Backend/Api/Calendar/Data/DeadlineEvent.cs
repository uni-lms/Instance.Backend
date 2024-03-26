namespace Aip.Instance.Backend.Api.Calendar.Data;

public class DeadlineEvent : IEvent {
  public required string Title { get; set; }
  public Guid Id { get; set; }
  public string Type => "deadline";
}