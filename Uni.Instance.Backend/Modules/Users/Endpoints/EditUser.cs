using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using Uni.Backend.Data;
using Uni.Backend.Modules.Static.Contracts;
using Uni.Backend.Modules.Static.Services;
using Uni.Backend.Modules.Users.Contracts;


namespace Uni.Backend.Modules.Users.Endpoints;

public class EditUser : Endpoint<EditUserRequest, UserDto, UserMapper> {
  private readonly AppDbContext _db;

  public EditUser(AppDbContext db) {
    _db = db;
  }

  public override void Configure() {
    Version(1);
    Patch("/users");
    AllowFileUploads();
    Options(x => x.WithTags("Users"));
    Description(b => b
      .Produces(200)
      .ProducesProblemFE(401)
      .ProducesProblemFE(404)
      .ProducesProblemFE(500));
    Summary(x => {
      x.Summary = "Edits user";
      x.Description = "<b>Allowed scopes:</b> Any authorized user";
      x.Responses[200] = "User updated successfully";
      x.Responses[401] = "Not authorized";
      x.Responses[404] = "User (or related entity) was not found";
      x.Responses[500] = "Some other error occured";
    });
  }

  public override async Task HandleAsync(EditUserRequest req, CancellationToken ct) {
    var user = await _db.Users.Where(e => e.Id == req.Id).FirstOrDefaultAsync(ct);

    if (user is null) {
      ThrowError(e => e.Id, "User with this id was not found", 404);
    }

    user.FirstName = req.FirstName;
    user.LastName = req.LastName;
    user.Patronymic = req.Patronymic;

    await _db.SaveChangesAsync(ct);

    await SendAsync(Map.FromEntity(user), cancellation: ct);
  }
}