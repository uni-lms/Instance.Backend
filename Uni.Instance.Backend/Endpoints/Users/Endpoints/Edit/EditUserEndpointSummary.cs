using Ardalis.Result;

using FastEndpoints;

using Uni.Instance.Backend.Configuration.Swagger;
using Uni.Instance.Backend.Endpoints.Users.Data;


namespace Uni.Instance.Backend.Endpoints.Users.Endpoints.Edit;

public class EditUserEndpointSummary : Summary<EditUserEndpoint> {
  public EditUserEndpointSummary() {
    Summary = "Редактирует пользователя";
    Description = CanBeUsedBy.AnyAdmin;
    Response<Result<EditUserResponse>>(200, "Пользователь успешно отредактирован");
    Response<Result<ErrorResponse>>(400, "Ошибка валидации");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
    Response<Result<ErrorResponse>>(404, "Пользователь не найден");
  }
}