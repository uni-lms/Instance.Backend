using System.Net.Mime;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using Uni.Backend.Configuration;
using Uni.Backend.Data;
using Uni.Backend.Modules.Common.Contracts;
using Uni.Backend.Modules.Groups.Contracts;
using Uni.Backend.Modules.Users.Contracts;


namespace Uni.Backend.Modules.Groups.Endpoints; 

public class GetGroup: Endpoint<SearchEntityRequest, GroupDto, GroupMapper> {
  private readonly AppDbContext _db;

  public GetGroup(AppDbContext db) {
    _db = db;
  }

  public override void Configure() {
    Version(1);
    Get("/groups/{id}");
    Roles(UserRoles.Administrator);
    Options(x => x.WithTags("Groups"));
    Description(b => b
      .Produces<UserDto>(200, MediaTypeNames.Application.Json)
      .ProducesProblemFE(401)
      .ProducesProblemFE(403)
      .ProducesProblemFE(500));
    Summary(x => {
      x.Summary = "Get group by id";
      x.Description = "<b>Allowed scopes:</b> Administrator";
      x.Responses[200] = "Group fetched successfully";
      x.Responses[401] = "Not authorized";
      x.Responses[403] = "Access forbidden";
      x.Responses[404] = "Not found";
      x.Responses[500] = "Some other error occured";
    });
  }

  public override async Task HandleAsync(SearchEntityRequest req, CancellationToken ct) {
    var result = await _db.Groups.AsNoTracking()
      .Where(e => e.Id == req.Id)
      .Include(e => e.Students)
      .Select(e => Map.FromEntity(e)).FirstOrDefaultAsync(ct);

    if (result is null) {
      await SendNotFoundAsync(ct);
      return;
    }
    
    await SendAsync(result, cancellation: ct);
  }
}