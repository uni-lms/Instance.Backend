using Aip.Instance.Backend.Api.Flows.Data;
using Aip.Instance.Backend.Api.Flows.Services;
using Aip.Instance.Backend.Configuration.Swagger;
using Aip.Instance.Backend.Data.Common;
using Aip.Instance.Backend.Extensions;

using Ardalis.Result;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Flows.Endpoints.Get;

public class GetFlowEndpoint(FlowsService service) : Endpoint<SearchByIdModel, Result<List<FlowDto>>> {
  public override void Configure() {
    Version(2);
    Get("/flows/{id}");
    Options(x => x.WithTags(ApiTags.Flows.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(SearchByIdModel req, CancellationToken ct) {
    var result = await service.GetGroupByIdAsync(req, ct);

    await this.SendResponseAsync(result, ct);
  }
}