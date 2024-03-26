using Aip.Instance.Backend.Api.Content.Text.Data;
using Aip.Instance.Backend.Configuration.Swagger;

using Ardalis.Result;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Content.Text.Endpoints.Update;

public class UpdateTextContentEndpointSummary : Summary<UpdateTextContentEndpoint> {
  public UpdateTextContentEndpointSummary() {
    Summary = "Обновляет текстовый контент на стажировку";
    Description = CanBeUsedBy.AnyInvitedTutor;
    Response<Result<CreateTextContentResponse>>(200, "Новые контент добавлен");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
    Response<Result<ErrorResponse>>(404, "Курс / раздел курса не найден");
    Response<Result<ErrorResponse>>(500, "Ошибка ввода-вывода");
  }
}