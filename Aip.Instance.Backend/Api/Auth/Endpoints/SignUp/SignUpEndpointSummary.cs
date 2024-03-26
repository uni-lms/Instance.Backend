using Aip.Instance.Backend.Api.Auth.Data;
using Aip.Instance.Backend.Configuration.Swagger;

using Ardalis.Result;

using FastEndpoints;

using JetBrains.Annotations;


namespace Aip.Instance.Backend.Api.Auth.Endpoints.SignUp;

[UsedImplicitly]
public class SignUpEndpointSummary : Summary<SignUpEndpoint> {
  public SignUpEndpointSummary() {
    Summary = "Создаёт нового пользователя и возвращает его токен доступа";
    Description = CanBeUsedBy.Anonymous;
    ExampleRequest = new SignUpRequest {
      Email = "foo@bar.com",
      Password = "p@ssW0rd",
      FirstName = "Иван",
      LastName = "Иванов",
    };
    Response<Result<LogInResponse>>(200, "Успешная регистрация");
    Response<Result<ErrorResponse>>(400, "Неверный запрос");
    Response<Result<ErrorResponse>>(404, "Несуществующая роль");
    Response<Result<ErrorResponse>>(409, "Пользователь с таким Email уже существует");
  }
}