using Aip.Instance.Backend.Api.Internships.Data;
using Aip.Instance.Backend.Api.Internships.Services;
using Aip.Instance.Backend.Configuration.Swagger;
using Aip.Instance.Backend.Data.Common;
using Aip.Instance.Backend.Extensions;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Internships.Endpoints.Delete;

public class DeleteInternshipEndpoint(InternshipsService service) : Endpoint<SearchByIdModel, InternshipDto> {
  public override void Configure() {
    Version(2);
    Delete("/internships/{id}");
    Options(x => x.WithTags(ApiTags.Internships.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(SearchByIdModel req, CancellationToken ct) {
    var result = await service.DeleteInternship(req, ct);
    await this.SendResponseAsync(result, ct);
  }
}