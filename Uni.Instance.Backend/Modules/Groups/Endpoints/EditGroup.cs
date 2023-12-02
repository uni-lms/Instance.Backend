using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using Uni.Backend.Configuration;
using Uni.Backend.Data;
using Uni.Backend.Modules.Groups.Contracts;


namespace Uni.Backend.Modules.Groups.Endpoints;

public class EditGroup : Endpoint<EditGroupRequest, GroupDto, GroupMapper> {
  private readonly AppDbContext _db;

  public EditGroup(AppDbContext db) {
    _db = db;
  }

  public override void Configure() {
    Version(1);
    Roles(UserRoles.Administrator);
    Put("/groups");
    Options(x => x.WithTags("Groups"));
    Description(b => b
      .Produces(200)
      .ProducesProblemFE(401)
      .ProducesProblemFE(403)
      .ProducesProblemFE(404)
      .ProducesProblemFE(500));
    Summary(x => {
      x.Summary = "Edits group";
      x.Description = "<b>Allowed scopes:</b> Administrator";
      x.Responses[200] = "Group updated successfully";
      x.Responses[401] = "Not authorized";
      x.Responses[403] = "Forbidden";
      x.Responses[404] = "Group was not found";
      x.Responses[500] = "Some other error occured";
    });
  }

  public override async Task HandleAsync(EditGroupRequest req, CancellationToken ct) {
    var group = await _db.Groups
      .Include(e => e.Students)
      .Where(e => e.Id == req.Id)
      .FirstOrDefaultAsync(ct);

    if (group is null) {
      ThrowError(e => e.Id, "Group was not found", 404);
    }

    group.Name = req.Name;
    group.CurrentSemester = req.CurrentSemester;
    group.MaxSemester = req.MaxSemester;

    await _db.SaveChangesAsync(ct);
    await SendAsync(Map.FromEntity(group), cancellation: ct);
  }
}