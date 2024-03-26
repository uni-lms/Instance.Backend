using Aip.Instance.Backend.Configuration.Swagger;

using Ardalis.Result;

using FastEndpoints;

using FileInfo = Aip.Instance.Backend.Api.Content.File.Data.FileInfo;


namespace Aip.Instance.Backend.Api.Content.File.Endpoints.Get;

public class GetFileContentInfoEndpointSummary : Summary<GetFileContentInfoEndpoint> {
  public GetFileContentInfoEndpointSummary() {
    Summary = "Получает информацию о файловом контенте";
    Description = CanBeUsedBy.AnyAuthorized;
    Response<Result<FileInfo>>(200, "Получена информация о файловом контенте");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(404, "Файл не найден");
  }
}