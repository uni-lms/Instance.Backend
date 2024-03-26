using Aip.Instance.Backend.Api.Content.Assignment.Data;
using Aip.Instance.Backend.Configuration.Swagger;

using Ardalis.Result;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Content.Assignment.Endpoints.GetById;

public class GetAssignmentByIdEndpointSummary : Summary<GetAssignmentByIdEndpoint> {
  public GetAssignmentByIdEndpointSummary() {
    Summary = "Получает информацию о задании";
    Description = CanBeUsedBy.AnyAuthorized;
    Response<Result<AssignmentInfo>>(200, "Получена информация о задании");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
    Response<Result<ErrorResponse>>(404, "Стажировка не найдена");
    Response<Result<ErrorResponse>>(500, "Ошибка ввода-вывода");
  }
}