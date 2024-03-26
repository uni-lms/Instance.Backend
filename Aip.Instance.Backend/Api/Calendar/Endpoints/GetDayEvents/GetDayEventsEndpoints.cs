using Aip.Instance.Backend.Api.Calendar.Data;
using Aip.Instance.Backend.Api.Calendar.Services;
using Aip.Instance.Backend.Configuration.Swagger;
using Aip.Instance.Backend.Extensions;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Calendar.Endpoints.GetDayEvents;

public class GetDayEventsEndpoint(CalendarService service) : Endpoint<DayEventsRequest> {
  public override void Configure() {
    Version(2);
    Get("/calendar/{year}/{month}/{day}");
    Options(x => x.WithTags(ApiTags.Calendar.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(DayEventsRequest req, CancellationToken ct) {
    var result = await service.GetEventsInDay(User, req, ct);
    await this.SendResponseAsync(result, ct);
  }
}