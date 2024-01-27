using Ardalis.Result;

using FastEndpoints;

using Uni.Instance.Backend.Api.Groups.Data;
using Uni.Instance.Backend.Configuration.Swagger;


namespace Uni.Instance.Backend.Api.Course.Endpoints.Create;

public class CreateCourseEndpointSummary : Summary<CreateCourseEndpoint> {
  public CreateCourseEndpointSummary() {
    Summary = "Создаёт новый курс";
    Description = CanBeUsedBy.AnyTutor;
    Response<Result<CreateGroupResponse>>(200, "Курс успешно создан");
    Response<Result<ErrorResponse>>(400, "Ошибка валидации");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
  }
}