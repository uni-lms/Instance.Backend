using Aip.Instance.Backend.Api.Internships.Data;
using Aip.Instance.Backend.Api.Internships.Services;
using Aip.Instance.Backend.Configuration.Swagger;
using Aip.Instance.Backend.Extensions;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Internships.Endpoints.InviteIntern;

public class InviteInternEndpoint(InternshipsService service) : Endpoint<InviteInternRequest> {
  public override void Configure() {
    Version(2);
    Post("/internships/{id}/intern");
    Options(x => x.WithTags(ApiTags.Flows.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(InviteInternRequest req, CancellationToken ct) {
    var result = await service.InviteIntern(req, ct);
    await this.SendResponseAsync(result, ct);
  }
}