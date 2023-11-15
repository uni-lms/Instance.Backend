using System.Net.Mime;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using Uni.Backend.Data;
using Uni.Backend.Modules.Assignments.Contracts;
using Uni.Backend.Modules.Common.Contracts;


namespace Uni.Instance.Backend.Modules.Assignments.Endpoints; 

public class GetAssignmentInfo: Endpoint<SearchEntityRequest, AssignmentDto> {
  private readonly AppDbContext _db;

  public GetAssignmentInfo(AppDbContext db) {
    _db = db;
  }

  public override void Configure() {
    Version(1);
    Post("/assignments/{id}");
    Options(x => x.WithTags("Assignments"));
    Description(b => b
      .Produces<Assignment>(200, MediaTypeNames.Application.Json)
      .ProducesProblemFE(401)
      .ProducesProblemFE(404)
      .ProducesProblemFE(500));
    Summary(x => {
      x.Summary = "Gets info of assignment";
      x.Description = "<b>Allowed scopes:</b> Any authorized user";
      x.Responses[201] = "Information was fetched successfully";
      x.Responses[401] = "Unauthorized";
      x.Responses[403] = "Forbidden";
      x.Responses[404] = "Some related entity was not found";
      x.Responses[500] = "Some other error occured";
    });
  }

  public override async Task HandleAsync(SearchEntityRequest req, CancellationToken ct) {
    var assignment = await _db.Assignments
      .AsNoTracking()
      .Where(e => e.Id == req.Id)
      .FirstOrDefaultAsync(ct);

    if (assignment is null) {
      ThrowError(e => e.Id, "Assignment was not found");
    }

    var dto = new AssignmentDto {
      Title = assignment.Title,
      Description = assignment.Description,
      AvailableUntil = assignment.AvailableUntil,
      Id = assignment.Id,
    };

    await SendAsync(dto, cancellation: ct);
  }
}