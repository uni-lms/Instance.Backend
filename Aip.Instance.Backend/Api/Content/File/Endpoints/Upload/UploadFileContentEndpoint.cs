using Aip.Instance.Backend.Api.Content.File.Data;
using Aip.Instance.Backend.Api.Content.File.Services;
using Aip.Instance.Backend.Configuration.Swagger;
using Aip.Instance.Backend.Extensions;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Content.File.Endpoints.Upload;

public class UploadFileContentEndpoint(ContentFileService service) : Endpoint<UploadFileContentRequest> {
  public override void Configure() {
    Version(2);
    Post("/content/file");
    Options(x => x.WithTags(ApiTags.FileContent.Tag));
    AllowFileUploads();
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(UploadFileContentRequest req, CancellationToken ct) {
    var result = await service.UploadFile(req, ct);
    await this.SendResponseAsync(result, ct);
  }
}