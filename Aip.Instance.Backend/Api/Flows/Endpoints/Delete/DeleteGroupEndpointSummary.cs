using Aip.Instance.Backend.Api.Flows.Data;
using Aip.Instance.Backend.Configuration.Swagger;

using Ardalis.Result;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Flows.Endpoints.Delete;

public class DeleteGroupEndpointSummary : Summary<DeleteFlowEndpoint> {
  public DeleteGroupEndpointSummary() {
    Summary = "Перманентно удаляет группу";
    Description = CanBeUsedBy.AnyPrimaryTutor;
    Response<Result<EditFlowResponse>>(200, "Группа успешно удалена");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
  }
}