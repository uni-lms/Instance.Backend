using Ardalis.Result;

using FastEndpoints;

using Uni.Instance.Backend.Api.Groups.Data;
using Uni.Instance.Backend.Configuration.Swagger;


namespace Uni.Instance.Backend.Api.Groups.Endpoints.Delete;

public class DeleteGroupEndpointSummary : Summary<DeleteGroupEndpoint> {
  public DeleteGroupEndpointSummary() {
    Summary = "Перманентно удаляет группу";
    Description = CanBeUsedBy.AnyAdmin;
    Response<Result<EditGroupResponse>>(200, "Группа успешно удалена");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
  }
}