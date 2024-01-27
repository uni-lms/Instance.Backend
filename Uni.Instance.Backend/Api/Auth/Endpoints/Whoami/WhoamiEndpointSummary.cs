using Ardalis.Result;

using FastEndpoints;

using JetBrains.Annotations;

using Uni.Instance.Backend.Api.Auth.Data;
using Uni.Instance.Backend.Configuration.Swagger;


namespace Uni.Instance.Backend.Api.Auth.Endpoints.Whoami;

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