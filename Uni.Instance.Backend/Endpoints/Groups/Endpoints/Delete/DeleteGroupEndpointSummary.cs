using Ardalis.Result;

using FastEndpoints;

using Uni.Instance.Backend.Configuration.Swagger;
using Uni.Instance.Backend.Endpoints.Groups.Data;


namespace Uni.Instance.Backend.Endpoints.Groups.Endpoints.Delete;

public class DeleteGroupEndpointSummary : Summary<DeleteGroupEndpoint> {
  public DeleteGroupEndpointSummary() {
    Summary = "Перманентно удаляет группу";
    Description = CanBeUsedBy.AnyAdmin;
    Response<Result<CreateGroupResponse>>(200, "Группа успешно удалена");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
  }
}