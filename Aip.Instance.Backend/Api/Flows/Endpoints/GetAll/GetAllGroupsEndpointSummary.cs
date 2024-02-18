using Aip.Instance.Backend.Api.Flows.Data;
using Aip.Instance.Backend.Configuration.Swagger;

using Ardalis.Result;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Flows.Endpoints.GetAll;

public class GetAllGroupsEndpointSummary : Summary<GetAllFlowsEndpoint> {
  public GetAllGroupsEndpointSummary() {
    Summary = "Возвращает список групп";
    Description = CanBeUsedBy.AnyTutor;
    Response<Result<List<FlowDto>>>(200, "Список групп успешно получен");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
  }
}