using Aip.Instance.Backend.Api.Common.Data;
using Aip.Instance.Backend.Api.Internships.Services;
using Aip.Instance.Backend.Configuration.Swagger;
using Aip.Instance.Backend.Extensions;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Internships.Endpoints.InviteTutor;

public class InviteTutorEndpoint(InternshipsService service) : Endpoint<ListOfGuidsRequest> {
  public override void Configure() {
    Version(2);
    Post("/internships/invite-tutor");
    Options(x => x.WithTags(ApiTags.Internships.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(ListOfGuidsRequest req, CancellationToken ct) {
    var result = await service.InviteTutor(req, ct);
    await this.SendResponseAsync(result, ct);
  }
}