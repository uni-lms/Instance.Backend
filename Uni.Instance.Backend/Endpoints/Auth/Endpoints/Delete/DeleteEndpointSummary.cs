using Ardalis.Result;

using FastEndpoints;

using Uni.Instance.Backend.Configuration;
using Uni.Instance.Backend.Endpoints.Auth.Data;


namespace Uni.Instance.Backend.Endpoints.Auth.Endpoints.Delete;

public class DeleteEndpointSummary : Summary<DeleteEndpoint> {
  public DeleteEndpointSummary() {
    Summary = "Безвозвратно удаляет аккаунт текущего пользователя";
    Description = CanBeUsedBy.AnyAuthorized;
    Response<Result<DeleteResponse>>(200, "Успешное удаление аккаунта");
    Response<Result<ErrorResponse>>(404, "Несуществующий пользователь");
  }
}