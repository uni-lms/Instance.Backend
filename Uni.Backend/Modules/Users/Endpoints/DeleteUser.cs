using FastEndpoints;
using Uni.Backend.Configuration;
using Uni.Backend.Data;
using Uni.Backend.Modules.Common.Contracts;

namespace Uni.Backend.Modules.Users.Endpoints;

public class DeleteUser: Endpoint<SearchEntityRequest>
{

    private readonly AppDbContext _db;

    public DeleteUser(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Version(1);
        Roles(UserRoles.Administrator);
        Delete("/users/{Id}");
        Options(x => x.WithTags("Users"));
        Description(b => b
            .Produces(204)
            .ProducesProblemFE(401)
            .ProducesProblemFE(403)
            .ProducesProblemFE(404)
            .ProducesProblemFE(500));
        Summary(x =>
        {
            x.Summary = "Permanently deletes user";
            x.Description = "<b>Allowed scopes:</b> Administrator";
            x.Responses[204] = "User deleted successfully";
            x.Responses[401] = "Not authorized";
            x.Responses[403] = "Access forbidden";
            x.Responses[404] = "User was not found";
            x.Responses[500] = "Some other error occured";
        });
    }

    public override async Task HandleAsync(SearchEntityRequest req, CancellationToken ct)
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