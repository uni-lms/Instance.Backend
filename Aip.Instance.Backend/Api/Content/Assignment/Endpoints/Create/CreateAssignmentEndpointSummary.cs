using Aip.Instance.Backend.Api.Content.File.Data;
using Aip.Instance.Backend.Configuration.Swagger;

using Ardalis.Result;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Content.Assignment.Endpoints.Create;

public class CreateAssignmentEndpointSummary : Summary<CreateAssignmentEndpoint> {
  public CreateAssignmentEndpointSummary() {
    Summary = "Добавляет новое задание на стажировку";
    Description = CanBeUsedBy.AnyInvitedTutor;
    Response<Result<UploadFileContentResponse>>(200, "Новый контент добавлен");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
    Response<Result<ErrorResponse>>(404, "Курс / раздел курса не найден");
    Response<Result<ErrorResponse>>(500, "Ошибка ввода-вывода");
  }
}