using Aip.Instance.Backend.Api.Internships.Data;
using Aip.Instance.Backend.Api.Internships.Services;
using Aip.Instance.Backend.Configuration.Swagger;
using Aip.Instance.Backend.Extensions;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Internships.Endpoints.Create;

public class CreateIntershipEndpoint(InternshipsService service) : Endpoint<CreateInternshipRequest, InternshipDto> {
  public override void Configure() {
    Version(2);
    Post("/internships");
    Options(x => x.WithTags(ApiTags.Internships.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(CreateInternshipRequest req, CancellationToken ct) {
    var result = await service.CreateInternship(ValidationFailed, ValidationFailures, User, req, ct);
    await this.SendResponseAsync(result, ct);
  }
}