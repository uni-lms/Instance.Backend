using System.Net.Mime;
using System.Security.Claims;

using FastEndpoints;

using Uni.Backend.Modules.Auth.Contracts;


namespace Uni.Backend.Modules.Auth.Endpoints;

public class Whoami : EndpointWithoutRequest<WhoamiResponse> {
  public override void Configure() {
    Version(1);
    Get("/auth/whoami");
    Options(x => x.WithTags("Auth"));
    Description(b => b
      .Produces<WhoamiResponse>(200, MediaTypeNames.Application.Json)
      .ProducesProblemFE(401)
      .ProducesProblemFE(500));
    Summary(x => {
      x.Summary = "Gets email and role of current user";
      x.Description = "<b>Allowed scopes:</b> Any authorized user";
      x.Responses[200] = "Information successfully fetched";
      x.Responses[401] = "Unauthorized";
      x.Responses[500] = "Some other error occured";
    });
  }

  public override async Task HandleAsync(CancellationToken ct) {
    var result = new WhoamiResponse {
      Email = User.Identity?.Name ?? "",
      Role = User.FindAll(ClaimTypes.Role).ToList()[0].Value,
    };
    await SendAsync(result, cancellation: ct);
  }
}