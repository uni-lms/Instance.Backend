using System.Net.Mime;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using Uni.Backend.Configuration;
using Uni.Backend.Data;
using Uni.Backend.Modules.CourseBlocks.Contracts;
using Uni.Instance.Backend.Modules.CourseBlocks.Contracts;
using Uni.Instance.Backend.Modules.Courses.Contracts;


namespace Uni.Instance.Backend.Modules.CourseBlocks.Endpoints;

public class GetAllBlocks : EndpointWithoutRequest<List<CourseBlockMini>> {
  private readonly AppDbContext _db;

  public GetAllBlocks(AppDbContext db) {
    _db = db;
  }

  public override void Configure() {
    Version(1);
    Get("/blocks");
    Roles(UserRoles.MinimumRequired(UserRoles.Tutor));
    Options(x => x.WithTags("Blocks"));
    Description(b => b
      .Produces<List<CourseBlockMini>>(200, MediaTypeNames.Application.Json)
      .ProducesProblemFE(401)
      .ProducesProblemFE(403)
      .ProducesProblemFE(500));
    Summary(x => {
      x.Summary = "Gets list of all course blocks";
      x.Description = "<b>Allowed scopes:</b> Tutor, Administrator";
      x.Responses[200] = "List of blocks fetched successfully";
      x.Responses[401] = "Not authorized";
      x.Responses[403] = "Access forbidden";
      x.Responses[500] = "Some other error occured";
    });
  }

  public override async Task HandleAsync(CancellationToken ct) {
    var courses = await _db.CourseBlocks
      .AsNoTracking()
      .ToListAsync(ct);

    var result = courses.Select(e => new CourseBlockMini {
      Id = e.Id,
      Name = e.Name,
    }).ToList();

    await SendAsync(result, cancellation: ct);
  }
}