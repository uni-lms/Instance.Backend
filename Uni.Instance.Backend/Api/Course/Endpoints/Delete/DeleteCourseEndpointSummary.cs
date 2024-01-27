using Ardalis.Result;

using FastEndpoints;

using Uni.Instance.Backend.Api.Course.Data;
using Uni.Instance.Backend.Configuration.Swagger;


namespace Uni.Instance.Backend.Api.Course.Endpoints.Delete;

public class DeleteCourseEndpointSummary : Summary<DeleteCourseEndpoint> {
  public DeleteCourseEndpointSummary() {
    Summary = "Перманентно удаляет курс";
    Description = CanBeUsedBy.AtLeastTutor;
    Response<Result<BaseCourseDto>>(200, "Курс успешно удалён");
    Response<Result<ErrorResponse>>(400, "Ошибка валидации");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
  }
}