using Aip.Instance.Backend.Api.Flows.Data;
using Aip.Instance.Backend.Configuration.Swagger;

using Ardalis.Result;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Flows.Endpoints.Create;

public class CreateGroupEndpointSummary : Summary<CreateFlowEndpoint> {
  public CreateGroupEndpointSummary() {
    Summary = "Создаёт новую пустую группу";
    Description = CanBeUsedBy.AnyPrimaryTutor;
    Response<Result<CreateFlowResponse>>(200, "Группа успешно создана");
    Response<Result<ErrorResponse>>(400, "Ошибка валидации");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
  }
}