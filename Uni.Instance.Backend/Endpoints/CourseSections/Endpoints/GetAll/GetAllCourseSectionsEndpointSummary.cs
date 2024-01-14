using Ardalis.Result;

using FastEndpoints;

using JetBrains.Annotations;

using Uni.Instance.Backend.Configuration.Swagger;
using Uni.Instance.Backend.Endpoints.CourseSections.Data;


namespace Uni.Instance.Backend.Endpoints.CourseSections.Endpoints.GetAll;

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