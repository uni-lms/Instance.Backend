using Aip.Instance.Backend.Api.Flows.Services;
using Aip.Instance.Backend.Configuration.Swagger;
using Aip.Instance.Backend.Data.Common;
using Aip.Instance.Backend.Extensions;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Flows.Endpoints.Delete;

public class DeleteFlowEndpoint(FlowsService service) : Endpoint<SearchByIdModel> {
  public override void Configure() {
    Version(2);
    Delete("/flows/{id}");
    Options(x => x.WithTags(ApiTags.Flows.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(SearchByIdModel req, CancellationToken ct) {
    var result = await service.DeleteGroupAsync(req, ct);
    await this.SendResponseAsync(result, ct);
  }
}