using Aip.Instance.Backend.Api.Content.File.Data;
using Aip.Instance.Backend.Api.Content.File.Services;
using Aip.Instance.Backend.Configuration.Swagger;
using Aip.Instance.Backend.Extensions;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Content.File.Endpoints.Edit;

public class EditFileContentEndpoint(ContentFileService service) : Endpoint<EditFileContentRequest> {
  public override void Configure() {
    Version(2);
    Put("/content/file/{id}");
    Options(x => x.WithTags(ApiTags.FileContent.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(EditFileContentRequest req, CancellationToken ct) {
    var result = await service.EditFileAsync(req, ct);
    await this.SendResponseAsync(result, ct);
  }
}