using Aip.Instance.Backend.Configuration.Swagger;
using Aip.Instance.Backend.Data.Common;

using Ardalis.Result;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Content.Link.Endpoints.Delete;

public class DeleteLinkContentEndpointSummary : Summary<DeleteLinkContentEndpoint> {
  public DeleteLinkContentEndpointSummary() {
    Summary = "Удаляет ссылочный контент со стажировки";
    Description = CanBeUsedBy.AnyInvitedTutor;
    Response<Result<SearchByIdModel>>(200, "Контент удален");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
    Response<Result<ErrorResponse>>(404, "Курс / раздел курса не найден");
    Response<Result<ErrorResponse>>(500, "Ошибка ввода-вывода");
  }
}