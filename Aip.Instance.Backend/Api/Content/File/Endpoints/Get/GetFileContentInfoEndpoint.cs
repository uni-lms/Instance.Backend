using Aip.Instance.Backend.Api.Content.File.Services;
using Aip.Instance.Backend.Configuration.Swagger;
using Aip.Instance.Backend.Data.Common;
using Aip.Instance.Backend.Extensions;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Content.File.Endpoints.Get;

public class GetFileContentInfoEndpoint(ContentFileService service) : Endpoint<SearchByIdModel> {
  public override void Configure() {
    Version(2);
    Get("/content/file/{id}");
    Options(x => x.WithTags(ApiTags.FileContent.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(SearchByIdModel req, CancellationToken ct) {
    var result = await service.GetFileInfoAsync(req, ct);
    await this.SendResponseAsync(result, ct);
  }
}