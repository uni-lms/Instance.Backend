using Ardalis.Result;

using FastEndpoints;

using Uni.Instance.Backend.Api.Course.Data;
using Uni.Instance.Backend.Configuration.Swagger;


namespace Uni.Instance.Backend.Api.Course.Endpoints.AddOwnerToCourse;

public class AddOwnerToCourseEndpointSummary : Summary<AddOwnerToCourseEndpoint> {
  public AddOwnerToCourseEndpointSummary() {
    Summary = "Добавляет пользователей в список владельцев курса";
    Description = CanBeUsedBy.AnyTutor;
    Response<Result<CourseOwnersResponse>>(200, "Новые пользователи добавлены");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
    Response<Result<ErrorResponse>>(404, "Курс не найден");
  }
}