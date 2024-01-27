using FastEndpoints;

using Uni.Instance.Backend.Configuration.Swagger;
using Uni.Instance.Backend.Endpoints.Groups.Services;
using Uni.Instance.Backend.Extensions;


namespace Uni.Instance.Backend.Endpoints.Groups.Endpoints.GetAll;

public class GetAllGroupsEndpoint(GroupsService service) : Endpoint<EmptyRequest> {
  public override void Configure() {
    Version(2);
    Get("/groups");
    Roles(CanBeUsedBy.TutorAndAbove);
    Options(x => x.WithTags(ApiTags.Groups.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(EmptyRequest _, CancellationToken ct) {
    var result = await service.GetAllGroupsAsync(ct);
    await this.SendResponseAsync(result, ct);
  }
}