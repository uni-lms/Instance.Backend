using Ardalis.Result;

using FastEndpoints;

using JetBrains.Annotations;

using Uni.Instance.Backend.Configuration.Swagger;
using Uni.Instance.Backend.Endpoints.Auth.Data;


namespace Uni.Instance.Backend.Endpoints.Auth.Endpoints.Whoami;

[UsedImplicitly]
public class WhoamiEndpointSummary : Summary<WhoamiEndpoint> {
  public WhoamiEndpointSummary() {
    Summary = "Возвращает информацию о текущем пользователе";
    Description = CanBeUsedBy.AnyAuthorized;
    Response<Result<WhoamiResponse>>(200, "Успешное получение информации");
    Response<Result<ErrorResponse>>(401, "Необходима авторизация");
    Response<Result<ErrorResponse>>(404, "Несуществующий пользователь");
  }
}