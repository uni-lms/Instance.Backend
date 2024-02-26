using Aip.Instance.Backend.Api.Content.File.Data;
using Aip.Instance.Backend.Configuration.Swagger;

using Ardalis.Result;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Content.Text.Endpoints.Delete;

public class DeleteTextContentEndpointSummary : Summary<DeleteTextContentEndpoint> {
  public DeleteTextContentEndpointSummary() {
    Summary = "Удаляет текстовый контент со стажировки";
    Description = CanBeUsedBy.AnyInvitedTutor;
    Response<Result<UploadFileContentResponse>>(200, "Контент удалён");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
    Response<Result<ErrorResponse>>(404, "Контент не найден");
    Response<Result<ErrorResponse>>(500, "Ошибка ввода-вывода");
  }
}