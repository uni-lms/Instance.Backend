using Aip.Instance.Backend.Api.Content.Link.Data;
using Aip.Instance.Backend.Api.Content.Link.Services;
using Aip.Instance.Backend.Configuration.Swagger;
using Aip.Instance.Backend.Extensions;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Content.Link.Endpoints.Create;

public class CreateLinkContentEndpoint(LinkContentService service) : Endpoint<CreateLinkContentRequest> {
  public override void Configure() {
    Version(2);
    Post("/content/link");
    Options(x => x.WithTags(ApiTags.LinkContent.Tag));
    AllowFileUploads();
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(CreateLinkContentRequest req, CancellationToken ct) {
    var result = await service.CreateLinkContent(req, ct);
    await this.SendResponseAsync(result, ct);
  }
}