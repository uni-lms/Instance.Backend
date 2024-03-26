using Aip.Instance.Backend.Api.Users.Data;
using Aip.Instance.Backend.Api.Users.Services;
using Aip.Instance.Backend.Configuration.Swagger;
using Aip.Instance.Backend.Extensions;

using Ardalis.Result;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Users.Endpoints.Edit;

public class EditUserEndpoint(UsersService service) : Endpoint<EditUserRequest, Result<EditUserResponse>> {
  public override void Configure() {
    Version(2);
    Put("/user/{id}");
    Options(x => x.WithTags(ApiTags.Users.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(EditUserRequest req, CancellationToken ct) {
    var result = await service.EditUserAsync(ValidationFailed, ValidationFailures, req, ct);
    await this.SendResponseAsync(result, ct);
  }
}