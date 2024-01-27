using Ardalis.Result;

using FastEndpoints;

using Uni.Instance.Backend.Api.Groups.Data;
using Uni.Instance.Backend.Configuration.Swagger;


namespace Uni.Instance.Backend.Api.Course.Endpoints.Owned;

public class GetOwnedCoursesEndpointSummary : Summary<GetOwnedCoursesEndpoint> {
  public GetOwnedCoursesEndpointSummary() {
    Summary = "Возвращает список курсов, которыми владеет текущий пользователь";
    Description = CanBeUsedBy.AnyTutor;
    Response<Result<CreateGroupResponse>>(200, "Возвращает список курсов");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
  }
}