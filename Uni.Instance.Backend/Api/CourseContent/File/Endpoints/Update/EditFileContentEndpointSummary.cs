using Ardalis.Result;

using FastEndpoints;

using Uni.Instance.Backend.Api.CourseContent.File.Data;
using Uni.Instance.Backend.Configuration.Swagger;


namespace Uni.Instance.Backend.Api.CourseContent.File.Endpoints.Update;

public class EditFileContentEndpointSummary : Summary<EditFileContentEndpoint> {
  public EditFileContentEndpointSummary() {
    Summary = "Обновляет файловый контент на курсе";
    Description = CanBeUsedBy.AnyTutor;
    Response<Result<UploadFileContentResponse>>(200, "Контент обновлён");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
    Response<Result<ErrorResponse>>(404, "Файл не найден");
  }
}