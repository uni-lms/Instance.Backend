using System.Net.Mime;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using Uni.Backend.Configuration;
using Uni.Backend.Data;
using Uni.Backend.Modules.Common.Contracts;
using Uni.Backend.Modules.Users.Contracts;


namespace Uni.Instance.Backend.Modules.Users.Endpoints;

public class GetUser : Endpoint<SearchEntityRequest, UserDto, UserMapper> {
  private readonly AppDbContext _db;

  public GetUser(AppDbContext db) {
    _db = db;
  }

  public override void Configure() {
    Version(1);
    Get("/users/{id}");
    Roles(UserRoles.Administrator);
    Options(x => x.WithTags("Users"));
    Description(b => b
      .Produces<UserDto>(200, MediaTypeNames.Application.Json)
      .ProducesProblemFE(401)
      .ProducesProblemFE(403)
      .ProducesProblemFE(500));
    Summary(x => {
      x.Summary = "Get user by id";
      x.Description = "<b>Allowed scopes:</b> Administrator";
      x.Responses[200] = "User fetched successfully";
      x.Responses[401] = "Not authorized";
      x.Responses[403] = "Access forbidden";
      x.Responses[404] = "Not found";
      x.Responses[500] = "Some other error occured";
    });
  }

  public override async Task HandleAsync(SearchEntityRequest req, CancellationToken ct) {
    var result = await _db.Users.AsNoTracking()
      .Include(e => e.Role)
      .Where(e => e.Id == req.Id)
      .Select(e => Map.FromEntity(e)).FirstOrDefaultAsync(ct);

    if (result is null) {
      await SendNotFoundAsync(ct);
      return;
    }
    
    await SendAsync(result, cancellation: ct);
  }
}