using Ardalis.Result;

using FastEndpoints;

using JetBrains.Annotations;

using Uni.Instance.Backend.Api.CourseSections.Data;
using Uni.Instance.Backend.Configuration.Swagger;


namespace Uni.Instance.Backend.Api.CourseSections.Endpoints.GetAll;

[UsedImplicitly]
public class GetAllCourseSectionsEndpointSummary : Summary<GetAllCourseSectionsEndpoint> {
  public GetAllCourseSectionsEndpointSummary() {
    Summary = "Возвращает список доступных секций курса";
    Description = CanBeUsedBy.AnyAuthorized;
    Response<Result<GetAllCourseSectionsResponse>>(200, "Успешное получение информации");
    Response<Result<ErrorResponse>>(401, "Необходима авторизация");
    Response<Result<ErrorResponse>>(404, "Несуществующий пользователь");
  }
}