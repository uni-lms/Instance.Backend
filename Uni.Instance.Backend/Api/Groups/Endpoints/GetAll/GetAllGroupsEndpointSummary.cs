using Ardalis.Result;

using FastEndpoints;

using Uni.Instance.Backend.Api.Groups.Data;
using Uni.Instance.Backend.Configuration.Swagger;


namespace Uni.Instance.Backend.Api.Groups.Endpoints.GetAll;

public class GetAllGroupsEndpointSummary : Summary<GetAllGroupsEndpoint> {
  public GetAllGroupsEndpointSummary() {
    Summary = "Возвращает список групп";
    Description = CanBeUsedBy.AtLeastTutor;
    Response<Result<List<GroupDto>>>(200, "Список групп успешно получен");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
  }
}