using Ardalis.Result;

using FastEndpoints;

using Uni.Instance.Backend.Api.Groups.Data;
using Uni.Instance.Backend.Configuration.Swagger;


namespace Uni.Instance.Backend.Api.Groups.Endpoints.Create;

public class CreateGroupEndpointSummary : Summary<CreateGroupEndpoint> {
  public CreateGroupEndpointSummary() {
    Summary = "Создаёт новую пустую группу";
    Description = CanBeUsedBy.AnyAdmin;
    Response<Result<CreateGroupResponse>>(200, "Группа успешно создана");
    Response<Result<ErrorResponse>>(400, "Ошибка валидации");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
  }
}