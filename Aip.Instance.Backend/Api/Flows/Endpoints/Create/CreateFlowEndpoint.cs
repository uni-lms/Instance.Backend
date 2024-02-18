using Aip.Instance.Backend.Api.Flows.Data;
using Aip.Instance.Backend.Api.Flows.Services;
using Aip.Instance.Backend.Configuration.Swagger;
using Aip.Instance.Backend.Data.Common;
using Aip.Instance.Backend.Extensions;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Flows.Endpoints.Create;

public class CreateFlowEndpoint(FlowsService service) : Endpoint<CreateFlowRequest, BaseModel> {
  public override void Configure() {
    Version(2);
    Post("/flows");
    Options(x => x.WithTags(ApiTags.Flows.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(CreateFlowRequest req, CancellationToken ct) {
    var result = await service.CreateGroupAsync(ValidationFailed, ValidationFailures, req, ct);
    await this.SendResponseAsync(result, ct);
  }
}