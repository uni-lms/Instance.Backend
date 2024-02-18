using Aip.Instance.Backend.Configuration.Swagger;

using Ardalis.Result;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Content.File.Endpoints.Download;

public class DownloadFileContentEndpointSummary : Summary<DownloadFileContentEndpoint> {
  public DownloadFileContentEndpointSummary() {
    Summary = "Отдаёт поток с запрошенным файлом";
    Description = CanBeUsedBy.AnyAuthorized;
    Response<FileStream>(200, "Контент загружается");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
    Response<Result<ErrorResponse>>(404, "Файл не найден");
  }
}