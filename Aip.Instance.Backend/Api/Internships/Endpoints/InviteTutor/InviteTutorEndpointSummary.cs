using Aip.Instance.Backend.Api.Internships.Data;
using Aip.Instance.Backend.Configuration.Swagger;

using Ardalis.Result;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Internships.Endpoints.InviteTutor;

public class InviteTutorEndpointSummary : Summary<InviteTutorEndpoint> {
  public InviteTutorEndpointSummary() {
    Summary = "Добавляет пользователей в список владельцев курса";
    Description = CanBeUsedBy.AnyInvitedTutor;
    Response<Result<InternshipDto>>(200, "Новые пользователи добавлены");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
    Response<Result<ErrorResponse>>(404, "Курс не найден");
  }
}