using Aip.Instance.Backend.Api.Users.Data;
using Aip.Instance.Backend.Configuration.Swagger;

using Ardalis.Result;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Users.Endpoints.Edit;

public class EditUserEndpointSummary : Summary<EditUserEndpoint> {
  public EditUserEndpointSummary() {
    Summary = "Редактирует пользователя";
    Description = CanBeUsedBy.AnyPrimaryTutor;
    ExampleRequest = new EditUserRequest {
      Email = "me@example.com",
      FirstName = "string",
      LastName = "string",
      Patronymic = null,
    };
    Response<Result<EditUserResponse>>(200, "Пользователь успешно отредактирован");
    Response<Result<ErrorResponse>>(400, "Ошибка валидации");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
    Response<Result<ErrorResponse>>(404, "Пользователь не найден");
  }
}