using Ardalis.Result;

using FastEndpoints;

using Uni.Instance.Backend.Api.Groups.Data;
using Uni.Instance.Backend.Api.Groups.Services;
using Uni.Instance.Backend.Configuration.Swagger;
using Uni.Instance.Backend.Data.Common;
using Uni.Instance.Backend.Extensions;


namespace Uni.Instance.Backend.Api.Groups.Endpoints.Get;

public class GetGroupEndpoint(GroupsService service) : Endpoint<SearchByIdModel, Result<List<GroupDto>>> {
  public override void Configure() {
    Version(2);
    Get("/groups/{id}");
    Roles(CanBeUsedBy.TutorAndAbove);
    Options(x => x.WithTags(ApiTags.Groups.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(SearchByIdModel req, CancellationToken ct) {
    var result = await service.GetGroupByIdAsync(req, ct);

    await this.SendResponseAsync(result, ct);
  }
}