using Ardalis.Result;

using FastEndpoints;

using Uni.Instance.Backend.Api.Users.Data;
using Uni.Instance.Backend.Api.Users.Services;
using Uni.Instance.Backend.Configuration.Swagger;
using Uni.Instance.Backend.Extensions;


namespace Uni.Instance.Backend.Api.Users.Endpoints.Edit;

public class EditUserEndpoint(UsersService service) : Endpoint<EditUserRequest, Result<EditUserResponse>> {
  public override void Configure() {
    Version(2);
    Put("/user/{id}");
    Roles(CanBeUsedBy.OnlyAdmin);
    Options(x => x.WithTags(ApiTags.Users.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(EditUserRequest req, CancellationToken ct) {
    var result = await service.EditUserAsync(ValidationFailed, ValidationFailures, req, ct);
    await this.SendResponseAsync(result, ct);
  }
}