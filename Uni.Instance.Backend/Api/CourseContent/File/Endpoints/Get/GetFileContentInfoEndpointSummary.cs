using Ardalis.Result;

using FastEndpoints;

using Uni.Instance.Backend.Api.CourseContent.File.Data;
using Uni.Instance.Backend.Configuration.Swagger;


namespace Uni.Instance.Backend.Api.CourseContent.File.Endpoints.Get;

public class GetFileContentInfoEndpointSummary : Summary<GetFileContentInfoEndpoint> {
  public GetFileContentInfoEndpointSummary() {
    Summary = "Получает информацию о файловом контенте";
    Description = CanBeUsedBy.AnyAuthorized;
    Response<Result<UploadFileContentResponse>>(200, "Получена информация о файловом контенте");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(404, "Файл не найден");
  }
}