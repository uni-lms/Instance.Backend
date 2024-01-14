using Ardalis.Result;

using FastEndpoints;

using JetBrains.Annotations;

using Uni.Instance.Backend.Configuration.Swagger;
using Uni.Instance.Backend.Endpoints.Auth.Data;


namespace Uni.Instance.Backend.Endpoints.Auth.Endpoints.Login;

[UsedImplicitly]
public class LoginEndpointSummary : Summary<LoginEndpoint> {
  public LoginEndpointSummary() {
    Summary = "Генерирует токен авторизации для пользователя по его почте и паролю";
    Description = CanBeUsedBy.Anonymous;
    ExampleRequest = new LoginRequest {
      Email = "me@example.com",
      Password = "1234",
    };
    Response<Result<LoginResponse>>(200, "Успешная генерация токена");
    Response<Result<ErrorResponse>>(400, "Неверный запрос");
    Response<Result<ErrorResponse>>(401, "Неверный пароль");
    Response<Result<ErrorResponse>>(404, "Несуществующий пользователь");
  }
}