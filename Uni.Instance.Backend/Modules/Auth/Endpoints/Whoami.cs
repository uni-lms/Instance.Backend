using System.Net.Mime;
using System.Security.Claims;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using Uni.Backend.Data;
using Uni.Instance.Backend.Modules.Auth.Contracts;


namespace Uni.Instance.Backend.Modules.Auth.Endpoints;

public class Whoami : EndpointWithoutRequest<WhoamiResponse> {
  private readonly AppDbContext _db;

  public Whoami(AppDbContext db) {
    _db = db;
  }

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
    var email = User.Identity?.Name;

    var user = await _db.Users.Where(e => e.Email == email).FirstOrDefaultAsync(ct);

    if (user is not null) {
      var result = new WhoamiResponse {
        Email = email ?? "",
        Role = User.FindAll(ClaimTypes.Role).ToList()[0].Value,
        FullName = $"{user.FirstName} {user.LastName}",
      };
      await SendAsync(result, cancellation: ct);
    }
    else {
      await SendNotFoundAsync(ct);
    }
  }
}