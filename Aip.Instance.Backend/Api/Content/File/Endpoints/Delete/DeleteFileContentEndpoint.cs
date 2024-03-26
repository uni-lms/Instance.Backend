using Aip.Instance.Backend.Api.Content.File.Services;
using Aip.Instance.Backend.Configuration.Swagger;
using Aip.Instance.Backend.Data.Common;
using Aip.Instance.Backend.Extensions;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Content.File.Endpoints.Delete;

public class DeleteFileContentEndpoint(ContentFileService service) : Endpoint<SearchByIdModel> {
  public override void Configure() {
    Version(2);
    Delete("/content/file/{id}");
    Options(x => x.WithTags(ApiTags.FileContent.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(SearchByIdModel req, CancellationToken ct) {
    var result = await service.DeleteFileAsync(req, ct);
    await this.SendResponseAsync(result, ct);
  }
}