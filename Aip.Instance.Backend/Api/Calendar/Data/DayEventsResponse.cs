namespace Aip.Instance.Backend.Api.Calendar.Data;

public class DayEventsResponse {
  public int Day { get; set; }
  public int Month { get; set; }
  public int Year { get; set; }
  public required IEnumerable<IEvent> Events { get; set; }
}