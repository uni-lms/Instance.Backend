using Aip.Instance.Backend.Api.Content.Text.Data;
using Aip.Instance.Backend.Configuration.Swagger;

using Ardalis.Result;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Content.Link.Endpoints.Update;

public class UpdateLinkContentEndpointSummary : Summary<UpdateLinkContentEndpoint> {
  public UpdateLinkContentEndpointSummary() {
    Summary = "Обновляет ссылочный контент на стажировке";
    Description = CanBeUsedBy.AnyInvitedTutor;
    Response<Result<CreateTextContentResponse>>(200, "контент обновлен");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
    Response<Result<ErrorResponse>>(404, "Курс / раздел курса не найден");
    Response<Result<ErrorResponse>>(500, "Ошибка ввода-вывода");
  }
}