using Aip.Instance.Backend.Api.Auth.Data;
using Aip.Instance.Backend.Configuration.Swagger;

using Ardalis.Result;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Auth.Endpoints.Delete;

public class DeleteEndpointSummary : Summary<DeleteUserEndpoint> {
  public DeleteEndpointSummary() {
    Summary = "Безвозвратно удаляет аккаунт текущего пользователя";
    Description = CanBeUsedBy.AnyAuthorized;
    Response<Result<DeleteUserResponse>>(200, "Успешное удаление аккаунта");
    Response<Result<ErrorResponse>>(404, "Несуществующий пользователь");
  }
}