using Aip.Instance.Backend.Api.Internships.Data;
using Aip.Instance.Backend.Configuration.Swagger;

using Ardalis.Result;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Internships.Endpoints.Delete;

public class DeleteInternshipEndpointSummary : Summary<DeleteInternshipEndpoint> {
  public DeleteInternshipEndpointSummary() {
    Summary = "Перманентно удаляет курс";
    Description = CanBeUsedBy.AnyTutor;
    Response<Result<InternshipDto>>(200, "Курс успешно удалён");
    Response<Result<ErrorResponse>>(400, "Ошибка валидации");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
  }
}