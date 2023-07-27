using Backend.Configuration;
using Backend.Data;
using Backend.Modules.Users.Contract;
using FastEndpoints;

namespace Backend.Modules.Users.Endpoints;

public class DeleteUser: Endpoint<SearchUserRequest>
{

    private readonly AppDbContext _db;

    public DeleteUser(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Delete("/users/{Id}");
        Options(x => x.WithTags("Static"));
        Roles(UserRoles.Administrator);
    }

    public override async Task HandleAsync(SearchUserRequest req, CancellationToken ct)
    {
        
        var user = await _db.Users.FindAsync(new object?[] { req.Id }, cancellationToken: ct);

        if (user is null)
        {
            ThrowError("User was not found", 404);
        }
        
        _db.Users.Remove(user);
        await _db.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
    }
}