using Ardalis.Result;

using FastEndpoints;

using JetBrains.Annotations;

using Uni.Instance.Backend.Configuration.Swagger;
using Uni.Instance.Backend.Endpoints.Groups.Data;


namespace Uni.Instance.Backend.Endpoints.Groups.Endpoints.Get;

[UsedImplicitly]
public class GetGroupEndpointSummary : Summary<GetGroupEndpoint> {
  public GetGroupEndpointSummary() {
    Summary = "Возвращает модель группы по идентификатору";
    Description = CanBeUsedBy.AnyAuthorized;
    Response<Result<GroupDto>>(200, "Упрощённая модель группы");
    Response<Result<ErrorResponse>>(400, "Ошибка валидации");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
    Response<Result<ErrorResponse>>(404, "Группа не найдена");
  }
}