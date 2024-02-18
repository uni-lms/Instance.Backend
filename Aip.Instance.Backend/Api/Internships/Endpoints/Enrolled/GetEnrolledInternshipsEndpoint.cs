using Aip.Instance.Backend.Api.Internships.Services;
using Aip.Instance.Backend.Configuration.Swagger;
using Aip.Instance.Backend.Extensions;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Internships.Endpoints.Enrolled;

public class GetEnrolledInternshipsEndpoint(InternshipsService service) : Endpoint<EmptyRequest> {
  public override void Configure() {
    Version(2);
    Post("/internships/enrolled");
    Roles(CanBeUsedBy.OnlyIntern);
    Options(x => x.WithTags(ApiTags.Internships.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(EmptyRequest req, CancellationToken ct) {
    var result = await service.GetEnrolledInternships(User, ct);
    await this.SendResponseAsync(result, ct);
  }
}