using Aip.Instance.Backend.Api.Flows.Data;
using Aip.Instance.Backend.Configuration.Swagger;

using Ardalis.Result;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Flows.Endpoints.Edit;

public class EditGroupEndpointSummary : Summary<EditFlowEndpoint> {
  public EditGroupEndpointSummary() {
    Summary = "Редактирует группу";
    Description = CanBeUsedBy.AnyPrimaryTutor;
    Response<Result<EditFlowResponse>>(200, "Группа успешно отредактирована");
    Response<Result<ErrorResponse>>(400, "Ошибка валидации");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
    Response<Result<ErrorResponse>>(404, "Группа не найдена");
  }
}