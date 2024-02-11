using Ardalis.Result;

using FastEndpoints;

using Uni.Instance.Backend.Api.CourseContent.File.Data;
using Uni.Instance.Backend.Configuration.Swagger;


namespace Uni.Instance.Backend.Api.CourseContent.File.Endpoints.Delete;

public class DeleteFileContentEndpointSummary : Summary<DeleteFileContentEndpoint> {
  public DeleteFileContentEndpointSummary() {
    Summary = "Удаляет файловый контент из курса";
    Description = CanBeUsedBy.AnyTutor;
    Response<Result<UploadFileContentResponse>>(200, "Контент удалён");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
    Response<Result<ErrorResponse>>(404, "Файл не найден");
  }
}