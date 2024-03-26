using Aip.Instance.Backend.Api.Internships.Data;
using Aip.Instance.Backend.Configuration.Swagger;

using Ardalis.Result;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Internships.Endpoints.Create;

public class CreateInternshipEndpointSummary : Summary<CreateIntershipEndpoint> {
  public CreateInternshipEndpointSummary() {
    Summary = "Создаёт новый курс";
    Description = CanBeUsedBy.AnyInvitedTutor;
    Response<Result<InternshipDto>>(200, "Курс успешно создан");
    Response<Result<ErrorResponse>>(400, "Ошибка валидации");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
  }
}