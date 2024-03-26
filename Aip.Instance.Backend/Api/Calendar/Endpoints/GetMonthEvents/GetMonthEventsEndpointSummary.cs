using Aip.Instance.Backend.Api.Calendar.Data;
using Aip.Instance.Backend.Configuration.Swagger;

using Ardalis.Result;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Calendar.Endpoints.GetMonthEvents;

public class GetMonthEventsEndpointSummary : Summary<GetMonthEventsEndpoints> {
  public GetMonthEventsEndpointSummary() {
    Summary = "Получает информацию о событиях в месяце";
    Description = CanBeUsedBy.AnyAuthorized;
    Response<Result<MonthEventsResponse>>(200, "Получена информация о событиях в месяце");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
    Response<Result<ErrorResponse>>(404, "Стажировка не найдена");
    Response<Result<ErrorResponse>>(500, "Ошибка ввода-вывода");
  }
}