using Aip.Instance.Backend.Api.Internships.Services;
using Aip.Instance.Backend.Configuration.Swagger;
using Aip.Instance.Backend.Extensions;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Internships.Endpoints.Owned;

public class GetOwnedInternshipsEndpoint(InternshipsService service) : Endpoint<EmptyRequest> {
  public override void Configure() {
    Version(2);
    Get("/internships/owned");
    Options(x => x.WithTags(ApiTags.Internships.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(EmptyRequest req, CancellationToken ct) {
    var result = await service.GetOwnedInternships(User, ct);
    await this.SendResponseAsync(result, ct);
  }
}