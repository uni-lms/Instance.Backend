using Ardalis.Result;

using FastEndpoints;

using JetBrains.Annotations;

using Uni.Instance.Backend.Api.Auth.Data;
using Uni.Instance.Backend.Configuration.Swagger;


namespace Uni.Instance.Backend.Api.Auth.Endpoints.LogIn;

[UsedImplicitly]
public class LogInEndpointSummary : Summary<LogInEndpoint> {
  public LogInEndpointSummary() {
    Summary = "Генерирует токен авторизации для пользователя по его почте и паролю";
    Description = CanBeUsedBy.Anonymous;
    ExampleRequest = new LogInRequest {
      Email = "me@example.com",
      Password = "1234",
    };
    Response<Result<LogInResponse>>(200, "Успешная генерация токена");
    Response<Result<ErrorResponse>>(400, "Неверный запрос");
    Response<Result<ErrorResponse>>(401, "Неверный пароль");
    Response<Result<ErrorResponse>>(404, "Несуществующий пользователь");
  }
}