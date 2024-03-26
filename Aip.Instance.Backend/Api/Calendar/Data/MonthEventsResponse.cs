namespace Aip.Instance.Backend.Api.Calendar.Data;

public class MonthEventsResponse {
  public int Year { get; set; }
  public int Month { get; set; }
  public required List<DayEventsOverview> Days { get; set; }
}