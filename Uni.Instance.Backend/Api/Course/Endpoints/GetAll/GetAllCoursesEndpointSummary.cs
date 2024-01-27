using Ardalis.Result;

using FastEndpoints;

using Uni.Instance.Backend.Api.Course.Data;
using Uni.Instance.Backend.Configuration.Swagger;


namespace Uni.Instance.Backend.Api.Course.Endpoints.GetAll;

public class GetAllCoursesEndpointSummary : Summary<GetAllCoursesEndpoint> {
  public GetAllCoursesEndpointSummary() {
    Summary = "Возвращает список всех курсов";
    Description = CanBeUsedBy.AtLeastTutor;
    Response<Result<List<BaseCourseDto>>>(200, "Список курсов успешно получен");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
  }
}