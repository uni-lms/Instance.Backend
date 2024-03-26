using Aip.Instance.Backend.Api.Sections.Data;
using Aip.Instance.Backend.Configuration.Swagger;

using Ardalis.Result;

using FastEndpoints;

using JetBrains.Annotations;


namespace Aip.Instance.Backend.Api.Sections.Endpoints.GetAll;

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