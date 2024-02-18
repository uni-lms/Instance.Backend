using Aip.Instance.Backend.Api.Content.File.Data;
using Aip.Instance.Backend.Configuration.Swagger;

using Ardalis.Result;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Content.File.Endpoints.Edit;

public class EditFileContentEndpointSummary : Summary<EditFileContentEndpoint> {
  public EditFileContentEndpointSummary() {
    Summary = "Обновляет файловый контент на курсе";
    Description = CanBeUsedBy.AnyInvitedTutor;
    Response<Result<UploadFileContentResponse>>(200, "Контент обновлён");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
    Response<Result<ErrorResponse>>(404, "Файл не найден");
  }
}