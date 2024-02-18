using Aip.Instance.Backend.Api.Flows.Services;
using Aip.Instance.Backend.Configuration.Swagger;
using Aip.Instance.Backend.Extensions;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Flows.Endpoints.GetAll;

public class GetAllFlowsEndpoint(FlowsService service) : Endpoint<EmptyRequest> {
  public override void Configure() {
    Version(2);
    Get("/flows");
    Options(x => x.WithTags(ApiTags.Flows.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(EmptyRequest _, CancellationToken ct) {
    var result = await service.GetAllGroupsAsync(ct);
    await this.SendResponseAsync(result, ct);
  }
}