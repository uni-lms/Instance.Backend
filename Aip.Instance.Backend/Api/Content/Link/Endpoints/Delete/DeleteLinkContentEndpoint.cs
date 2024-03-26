using Aip.Instance.Backend.Api.Content.Link.Services;
using Aip.Instance.Backend.Configuration.Swagger;
using Aip.Instance.Backend.Data.Common;
using Aip.Instance.Backend.Extensions;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Content.Link.Endpoints.Delete;

public class DeleteLinkContentEndpoint(LinkContentService service) : Endpoint<SearchByIdModel> {
  public override void Configure() {
    Version(2);
    Delete("/content/link/{id}");
    Options(x => x.WithTags(ApiTags.LinkContent.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(SearchByIdModel req, CancellationToken ct) {
    var result = await service.DeleteLinkContent(req, ct);
    await this.SendResponseAsync(result, ct);
  }
}