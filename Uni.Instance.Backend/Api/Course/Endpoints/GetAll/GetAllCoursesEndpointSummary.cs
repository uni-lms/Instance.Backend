using Ardalis.Result;

using FastEndpoints;

using Uni.Instance.Backend.Api.Groups.Data;
using Uni.Instance.Backend.Configuration.Swagger;


namespace Uni.Instance.Backend.Api.Course.Endpoints.GetAll;

public class GetAllCoursesEndpointSummary : Summary<GetAllCoursesEndpoint> {
  public GetAllCoursesEndpointSummary() {
    Summary = "Получить список всех курсов";
    Description = CanBeUsedBy.AtLeastTutor;
    Response<Result<CreateGroupResponse>>(200, "Список курсов успешно получен");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
  }
}