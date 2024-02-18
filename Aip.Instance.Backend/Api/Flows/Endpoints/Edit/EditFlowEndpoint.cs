using Aip.Instance.Backend.Api.Flows.Data;
using Aip.Instance.Backend.Api.Flows.Services;
using Aip.Instance.Backend.Configuration.Swagger;
using Aip.Instance.Backend.Extensions;

using Ardalis.Result;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Flows.Endpoints.Edit;

public class EditFlowEndpoint(FlowsService service) : Endpoint<EditFlowRequest, Result<EditFlowResponse>> {
  public override void Configure() {
    Version(2);
    Put("/flows/{id}");
    Options(x => x.WithTags(ApiTags.Flows.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(EditFlowRequest req, CancellationToken ct) {
    var result = await service.EditGroupAsync(ValidationFailed, ValidationFailures, req, ct);
    await this.SendResponseAsync(result, ct);
  }
}