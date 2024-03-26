using Aip.Instance.Backend.Configuration.Swagger;
using Aip.Instance.Backend.Data.Common;

using Ardalis.Result;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Content.Assignment.Endpoints.Update;

public class UpdateAssignmentEndpointSummary : Summary<UpdateAssignmentEndpoint> {
  public UpdateAssignmentEndpointSummary() {
    Summary = "Обновляет задание на стажировке";
    Description = CanBeUsedBy.AnyInvitedTutor;
    Response<Result<SearchByIdModel>>(200, "контент обновлен");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
    Response<Result<ErrorResponse>>(404, "Курс / раздел курса не найден");
    Response<Result<ErrorResponse>>(500, "Ошибка ввода-вывода");
  }
}