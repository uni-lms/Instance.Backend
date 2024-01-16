using FastEndpoints;

using Uni.Instance.Backend.Configuration.Swagger;
using Uni.Instance.Backend.Endpoints.Groups.Data;
using Uni.Instance.Backend.Endpoints.Groups.Services;
using Uni.Instance.Backend.Extensions;


namespace Uni.Instance.Backend.Endpoints.Groups.Endpoints.Edit;

public class EditGroupEndpoint(GroupsService service) : Endpoint<EditGroupRequest> {
  public override void Configure() {
    Version(2);
    Post("/groups/{id}");
    Roles(CanBeUsedBy.OnlyAdmin);
    Options(x => x.WithTags(ApiTags.Groups.Tag));
  }

  public override async Task HandleAsync(EditGroupRequest req, CancellationToken ct) {
    var result = await service.EditGroupAsync(ValidationFailed, ValidationFailures, req, ct);
    await this.SendResponseAsync(result, ct);
  }
}