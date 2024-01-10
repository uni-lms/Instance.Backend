using Ardalis.Result;

using FastEndpoints;

using JetBrains.Annotations;

using Uni.Instance.Backend.Endpoints.Auth.Data;


namespace Uni.Instance.Backend.Endpoints.Auth.Endpoints.Whoami;

[UsedImplicitly]
public class WhoamiEndpointSummary : Summary<WhoamiEndpoint> {
  public WhoamiEndpointSummary() {
    Summary = "Возвращает информацию о текущем пользователе";
    Description = "<b>Может использоваться:</b> Любым авторизованным пользователем";
    Response<Result<WhoamiResponse>>(200, "Успешное получение информации");
    Response<Result<ErrorResponse>>(404, "Несуществующий пользователь");
  }
}