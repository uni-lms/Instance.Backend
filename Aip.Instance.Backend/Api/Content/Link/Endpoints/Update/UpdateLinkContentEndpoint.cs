using Aip.Instance.Backend.Api.Content.Link.Data;
using Aip.Instance.Backend.Api.Content.Link.Services;
using Aip.Instance.Backend.Configuration.Swagger;
using Aip.Instance.Backend.Extensions;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Content.Link.Endpoints.Update;

public class UpdateLinkContentEndpoint(LinkContentService service) : Endpoint<UpdateLinkContentRequest> {
  public override void Configure() {
    Version(2);
    Put("/content/link/{id}");
    Options(x => x.WithTags(ApiTags.LinkContent.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(UpdateLinkContentRequest req, CancellationToken ct) {
    var result = await service.UpdateLinkContent(req, ct);
    await this.SendResponseAsync(result, ct);
  }
}