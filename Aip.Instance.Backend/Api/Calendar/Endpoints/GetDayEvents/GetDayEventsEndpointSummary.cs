using Aip.Instance.Backend.Api.Calendar.Data;
using Aip.Instance.Backend.Configuration.Swagger;

using Ardalis.Result;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Calendar.Endpoints.GetDayEvents;

public class GetDayEventsEndpointSummary : Summary<GetDayEventsEndpoint> {
  public GetDayEventsEndpointSummary() {
    Summary = "Получает информацию о событиях за день";
    Description = CanBeUsedBy.AnyAuthorized;
    Response<Result<MonthEventsResponse>>(200, "Получена информация о событиях за день");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
    Response<Result<ErrorResponse>>(404, "Стажировка не найдена");
    Response<Result<ErrorResponse>>(500, "Ошибка ввода-вывода");
  }
}