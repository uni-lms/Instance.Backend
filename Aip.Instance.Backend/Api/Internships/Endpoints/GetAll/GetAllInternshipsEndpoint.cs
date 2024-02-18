using Aip.Instance.Backend.Api.Internships.Data;
using Aip.Instance.Backend.Api.Internships.Services;
using Aip.Instance.Backend.Configuration.Swagger;
using Aip.Instance.Backend.Extensions;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Internships.Endpoints.GetAll;

public class GetAllInternshipsEndpoint(InternshipsService service) : Endpoint<EmptyRequest, List<InternshipDto>> {
  public override void Configure() {
    Version(2);
    Get("/internships");
    Options(x => x.WithTags(ApiTags.Internships.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(EmptyRequest _, CancellationToken ct) {
    var result = await service.GetAllInternships(ct);
    await this.SendResponseAsync(result, ct);
  }
}