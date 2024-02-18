using Aip.Instance.Backend.Api.Internships.Data;
using Aip.Instance.Backend.Configuration.Swagger;

using Ardalis.Result;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Internships.Endpoints.Enrolled;

public class GetEnrolledInternshipsEndpointSummary : Summary<GetEnrolledInternshipsEndpoint> {
  public GetEnrolledInternshipsEndpointSummary() {
    Summary = "Возвращает список курсов, доступных для текущего пользователя, отфильтрованных по типу курса";
    Description =
      $"{CanBeUsedBy.AnyIntern}<br/><b>Допустимые значения фильтра:</b><ul><li><code>Archived</code> – Архивные курсы (из прошедших семестров)</li><li><code>Current</code> – Текущие курсы</li><li><code>Upcoming</code> – Предстоящие курсы</li>";
    Response<Result<List<InternshipDto>>>(200, "Успешное получение информации");
    Response<Result<ErrorResponse>>(401, "Необходима авторизация");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
    Response<Result<ErrorResponse>>(404, "Несуществующий пользователь");
  }
}