using Ardalis.Result;

using FastEndpoints;

using Uni.Instance.Backend.Configuration.Swagger;
using Uni.Instance.Backend.Endpoints.Groups.Data;


namespace Uni.Instance.Backend.Endpoints.Groups.Endpoints.Edit;

public class EditGroupEndpointSummary : Summary<EditGroupEndpoint> {
  public EditGroupEndpointSummary() {
    Summary = "Редактирует группу";
    Description = CanBeUsedBy.AnyAdmin;
    Response<Result<EditGroupResponse>>(200, "Группа успешно отредактирована");
    Response<Result<ErrorResponse>>(400, "Ошибка валидации");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
  }
}