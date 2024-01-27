using FastEndpoints;

using Uni.Instance.Backend.Configuration.Swagger;
using Uni.Instance.Backend.Data.Common;
using Uni.Instance.Backend.Endpoints.Groups.Services;
using Uni.Instance.Backend.Extensions;


namespace Uni.Instance.Backend.Endpoints.Groups.Endpoints.Delete;

public class DeleteGroupEndpoint(GroupsService service) : Endpoint<SearchByIdModel> {
  public override void Configure() {
    Version(2);
    Delete("/groups/{id}");
    Roles(CanBeUsedBy.OnlyAdmin);
    Options(x => x.WithTags(ApiTags.Groups.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(SearchByIdModel req, CancellationToken ct) {
    var result = await service.DeleteGroupAsync(req, ct);
    await this.SendResponseAsync(result, ct);
  }
}