using Aip.Instance.Backend.Api.Content.Link.Data;
using Aip.Instance.Backend.Configuration.Swagger;

using Ardalis.Result;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Content.Link.Endpoints.Create;

public class CreateLinkContentEndpointSummary : Summary<CreateLinkContentEndpoint> {
  public CreateLinkContentEndpointSummary() {
    Summary = "Добавляет новый ссылочный контент на курс";
    Description = CanBeUsedBy.AnyInvitedTutor;
    Response<Result<CreateLinkContentResponse>>(200, "Новые контент добавлен");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
    Response<Result<ErrorResponse>>(404, "Курс / раздел курса не найден");
    Response<Result<ErrorResponse>>(500, "Ошибка ввода-вывода");
  }
}