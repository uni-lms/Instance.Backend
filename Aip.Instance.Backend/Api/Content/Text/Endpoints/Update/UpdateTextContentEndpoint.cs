using Aip.Instance.Backend.Api.Content.Text.Data;
using Aip.Instance.Backend.Api.Content.Text.Services;
using Aip.Instance.Backend.Configuration.Swagger;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Content.Text.Endpoints.Update;

public class UpdateTextContentEndpoint(TextContentService service) : Endpoint<UpdateTextContentRequest> {
  public override void Configure() {
    Version(2);
    Put("/content/text/{id}");
    Options(x => x.WithTags(ApiTags.TextContent.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(UpdateTextContentRequest req, CancellationToken ct) {
    var result = await service.UpdateTextContent(req, ct);
  }
}