using Aip.Instance.Backend.Api.Content.Text.Data;
using Aip.Instance.Backend.Api.Content.Text.Services;
using Aip.Instance.Backend.Configuration.Swagger;
using Aip.Instance.Backend.Extensions;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Content.Text.Endpoints.Create;

public class CreateTextContentEndpoint(TextContentService service) : Endpoint<CreateTextContentRequest> {
  public override void Configure() {
    Version(2);
    Post("/content/text");
    Options(x => x.WithTags(ApiTags.TextContent.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(CreateTextContentRequest req, CancellationToken ct) {
    var result = await service.CreateTextContent(req, ct);
    await this.SendResponseAsync(result, ct);
  }
}