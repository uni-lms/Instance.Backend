using Ardalis.Result;

using FastEndpoints;

using Uni.Instance.Backend.Api.CourseContent.File.Data;
using Uni.Instance.Backend.Configuration.Swagger;


namespace Uni.Instance.Backend.Api.CourseContent.File.Endpoints.Upload;

public class UploadFileContentEndpointSummary : Summary<UploadFileContentEndpoint> {
  public UploadFileContentEndpointSummary() {
    Summary = "Добавляет новый файловый контент на курс";
    Description = CanBeUsedBy.AnyTutor;
    Response<Result<UploadFileContentResponse>>(200, "Новые контент добавлен");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
    Response<Result<ErrorResponse>>(404, "Курс / раздел курса не найден");
    Response<Result<ErrorResponse>>(500, "Ошибка ввода-вывода");
  }
}