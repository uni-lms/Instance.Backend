namespace Aip.Instance.Backend.Api.Calendar.Data;

public interface IEvent {
  public Guid Id { get; set; }
  public string Type { get; }
}