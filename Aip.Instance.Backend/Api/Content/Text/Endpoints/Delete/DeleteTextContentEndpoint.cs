using Aip.Instance.Backend.Api.Content.Text.Services;
using Aip.Instance.Backend.Configuration.Swagger;
using Aip.Instance.Backend.Data.Common;
using Aip.Instance.Backend.Extensions;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Content.Text.Endpoints.Delete;

public class DeleteTextContentEndpoint(TextContentService service) : Endpoint<SearchByIdModel> {
  public override void Configure() {
    Version(2);
    Delete("/content/text/{id}");
    Options(x => x.WithTags(ApiTags.TextContent.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(SearchByIdModel req, CancellationToken ct) {
    var result = await service.DeleteTextContent(req, ct);
    await this.SendResponseAsync(result, ct);
  }
}