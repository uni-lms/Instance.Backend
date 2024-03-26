using Aip.Instance.Backend.Api.Content.Assignment.Data;
using Aip.Instance.Backend.Api.Content.Assignment.Services;
using Aip.Instance.Backend.Configuration.Swagger;
using Aip.Instance.Backend.Extensions;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Content.Assignment.Endpoints.Create;

public class CreateAssignmentEndpoint(AssignmentService service) : Endpoint<CreateAssignmentRequest> {
  public override void Configure() {
    Version(2);
    Post("/content/assignments");
    Options(x => x.WithTags(ApiTags.Assignment.Tag));
    AllowFileUploads();
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(CreateAssignmentRequest req, CancellationToken ct) {
    var result = await service.CreateAssignment(req, ct);
    await this.SendResponseAsync(result, ct);
  }
}