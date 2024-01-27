using Ardalis.Result;

using FastEndpoints;

using Uni.Instance.Backend.Api.Auth.Data;
using Uni.Instance.Backend.Configuration.Swagger;


namespace Uni.Instance.Backend.Api.Auth.Endpoints.Delete;

public class DeleteEndpointSummary : Summary<DeleteEndpoint> {
  public DeleteEndpointSummary() {
    Summary = "Безвозвратно удаляет аккаунт текущего пользователя";
    Description = CanBeUsedBy.AnyAuthorized;
    Response<Result<DeleteUserResponse>>(200, "Успешное удаление аккаунта");
    Response<Result<ErrorResponse>>(404, "Несуществующий пользователь");
  }
}