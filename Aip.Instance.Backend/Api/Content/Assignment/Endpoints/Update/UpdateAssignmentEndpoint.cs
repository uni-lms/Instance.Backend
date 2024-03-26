using Aip.Instance.Backend.Api.Content.Assignment.Data;
using Aip.Instance.Backend.Api.Content.Assignment.Services;
using Aip.Instance.Backend.Configuration.Swagger;
using Aip.Instance.Backend.Extensions;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Content.Assignment.Endpoints.Update;

public class UpdateAssignmentEndpoint(AssignmentService service) : Endpoint<UpdateAssignmentRequest> {
  public override void Configure() {
    Version(2);
    Put("/content/assignments/{id}");
    Options(x => x.WithTags(ApiTags.Assignment.Tag));
    DontThrowIfValidationFails();
    AllowFileUploads();
  }

  public override async Task HandleAsync(UpdateAssignmentRequest req, CancellationToken ct) {
    var result = await service.UpdateAssignment(req, ct);
    await this.SendResponseAsync(result, ct);
  }
}