using Aip.Instance.Backend.Configuration.Swagger;

using Ardalis.Result;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Content.Common.Endpoints.Get;

public class GetContentEndpointSummary : Summary<GetContentEndpoint> {
  public GetContentEndpointSummary() {
    Summary = "Получает список контента на стажировке";
    Description = CanBeUsedBy.AnyAuthorized;
    Response<Result<Data.Content>>(200, "Получен контент");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(404, "Стажировка не найдена");
  }
}