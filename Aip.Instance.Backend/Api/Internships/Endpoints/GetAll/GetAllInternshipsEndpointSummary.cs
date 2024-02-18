using Aip.Instance.Backend.Api.Internships.Data;
using Aip.Instance.Backend.Configuration.Swagger;

using Ardalis.Result;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Internships.Endpoints.GetAll;

public class GetAllInternshipsEndpointSummary : Summary<GetAllInternshipsEndpoint> {
  public GetAllInternshipsEndpointSummary() {
    Summary = "Возвращает список всех курсов";
    Description = CanBeUsedBy.AnyTutor;
    Response<Result<List<InternshipDto>>>(200, "Список курсов успешно получен");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
  }
}