using Aip.Instance.Backend.Api.Content.Common.Services;
using Aip.Instance.Backend.Configuration.Swagger;
using Aip.Instance.Backend.Data.Common;
using Aip.Instance.Backend.Extensions;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Content.Common.Endpoints.Get;

public class GetContentEndpoint(ContentService service) : Endpoint<SearchByIdModel> {
  public override void Configure() {
    Version(2);
    Get("/content/{id}");
    Options(x => x.WithTags(ApiTags.Content.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(SearchByIdModel req, CancellationToken ct) {
    var result = await service.GetFlowContent(req, User, ct);
    await this.SendResponseAsync(result, ct);
  }
}