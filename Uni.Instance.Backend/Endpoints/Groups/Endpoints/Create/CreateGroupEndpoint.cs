using FastEndpoints;

using Uni.Instance.Backend.Configuration.Swagger;
using Uni.Instance.Backend.Data.Common;
using Uni.Instance.Backend.Endpoints.Groups.Data;
using Uni.Instance.Backend.Endpoints.Groups.Services;
using Uni.Instance.Backend.Extensions;


namespace Uni.Instance.Backend.Endpoints.Groups.Endpoints.Create;

public class CreateGroupEndpoint(GroupsService service) : Endpoint<CreateGroupRequest, BaseModel> {
  public override void Configure() {
    Version(2);
    Post("/groups");
    Roles(CanBeUsedBy.OnlyAdmin);
    Options(x => x.WithTags(ApiTags.Groups.Tag));
  }

  public override async Task HandleAsync(CreateGroupRequest req, CancellationToken ct) {
    var result = await service.CreateGroupAsync(ValidationFailed, ValidationFailures, req, ct);
    await this.SendResponseAsync(result, ct);
  }
}