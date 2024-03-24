using Aip.Instance.Backend.Api.Calendar.Data;
using Aip.Instance.Backend.Api.Calendar.Services;
using Aip.Instance.Backend.Configuration.Swagger;
using Aip.Instance.Backend.Extensions;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Calendar.Endpoints.GetMonthEvents;

public class GetMonthEventsEndpoints(CalendarService service) : Endpoint<MonthEventsRequest> {
  public override void Configure() {
    Version(2);
    Get("/calendar/{year}/{month}");
    Options(x => x.WithTags(ApiTags.Calendar.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(MonthEventsRequest req, CancellationToken ct) {
    var result = await service.GetEventsInMonth(User, req, ct);
    await this.SendResponseAsync(result, ct);
  }
}