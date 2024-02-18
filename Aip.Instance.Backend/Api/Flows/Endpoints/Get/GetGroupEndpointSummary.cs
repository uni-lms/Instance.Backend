using Aip.Instance.Backend.Api.Flows.Data;
using Aip.Instance.Backend.Configuration.Swagger;

using Ardalis.Result;

using FastEndpoints;

using JetBrains.Annotations;


namespace Aip.Instance.Backend.Api.Flows.Endpoints.Get;

[UsedImplicitly]
public class GetGroupEndpointSummary : Summary<GetFlowEndpoint> {
  public GetGroupEndpointSummary() {
    Summary = "Возвращает модель группы по идентификатору";
    Description = CanBeUsedBy.AnyTutor;
    Response<Result<FlowDto>>(200, "Упрощённая модель группы");
    Response<Result<ErrorResponse>>(400, "Ошибка валидации");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
    Response<Result<ErrorResponse>>(404, "Группа не найдена");
  }
}