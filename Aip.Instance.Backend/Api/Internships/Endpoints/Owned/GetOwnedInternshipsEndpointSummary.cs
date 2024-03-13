using Aip.Instance.Backend.Api.Internships.Data;
using Aip.Instance.Backend.Configuration.Swagger;

using Ardalis.Result;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Internships.Endpoints.Owned;

public class GetOwnedInternshipsEndpointSummary : Summary<GetOwnedInternshipsEndpoint> {
  public GetOwnedInternshipsEndpointSummary() {
    Summary = "Возвращает список курсов, которыми владеет текущий пользователь";
    Description = CanBeUsedBy.AnyInvitedTutor;
    Response<Result<List<InternshipDto>>>(200, "Возвращает список курсов");
    Response<Result<ErrorResponse>>(401, "Неавторизованный доступ");
    Response<Result<ErrorResponse>>(403, "Доступ запрещён");
  }
}