﻿using Ardalis.Result;

using FastEndpoints;

using JetBrains.Annotations;

using Uni.Instance.Backend.Configuration;
using Uni.Instance.Backend.Endpoints.Auth.Data;


namespace Uni.Instance.Backend.Endpoints.Auth.Endpoints.Signup;

[UsedImplicitly]
public class SignupEndpointSummary : Summary<SignupEndpoint> {
  public SignupEndpointSummary() {
    Summary = "Создаёт нового пользователя и возвращает его токен доступа";
    Description = CanBeUsedBy.Anonymous;
    ExampleRequest = new SignupRequest {
      Email = "foo@bar.com",
      Password = "p@ssW0rd",
      FirstName = "Иван",
      LastName = "Иванов",
      RoleId = new Guid("c82073b9-126c-4b4e-8edc-bc3d0cea56f1"),
    };
    Response<Result<LoginResponse>>(200, "Успешная регистрация");
    Response<Result<ErrorResponse>>(400, "Неверный запрос");
    Response<Result<ErrorResponse>>(404, "Несуществующая роль");
    Response<Result<ErrorResponse>>(409, "Пользователь с таким Email уже существует");
  }
}