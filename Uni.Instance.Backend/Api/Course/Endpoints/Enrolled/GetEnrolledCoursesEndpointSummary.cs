using Ardalis.Result;

using FastEndpoints;

using Uni.Instance.Backend.Api.Course.Data;
using Uni.Instance.Backend.Configuration.Swagger;


namespace Uni.Instance.Backend.Api.Course.Endpoints.Enrolled;

public class GetEnrolledCoursesEndpointSummary : Summary<GetEnrolledCoursesEndpoint> {
  public GetEnrolledCoursesEndpointSummary() {
    Summary = "Возвращает список курсов, доступных для текущего пользователя, отфильтрованных по типу курса";
    Description =
      $"{CanBeUsedBy.AnyStudent}<br/><b>Допустимые значения фильтра:</b><ul><li><code>Archived</code> – Архивные курсы (из прошедших семестров)</li><li><code>Current</code> – Текущие курсы</li><li><code>Upcoming</code> – Предстоящие курсы</li>";
    Response<Result<List<StudentCourseDto>>>(200, "Успешное получение информации");
    Response<Result<ErrorResponse>>(401, "Необходима авторизация");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
    Response<Result<ErrorResponse>>(404, "Несуществующий пользователь");
  }
}