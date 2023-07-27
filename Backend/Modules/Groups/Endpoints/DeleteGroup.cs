using Backend.Configuration;
using Backend.Data;
using Backend.Modules.Common.Contract;
using FastEndpoints;

namespace Backend.Modules.Groups.Endpoints;

public class DeleteGroup: Endpoint<SearchEntityRequest>
{

    private readonly AppDbContext _db;

    public DeleteGroup(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Delete("/groups");
        Options(x => x.WithTags("Groups"));
        Roles(UserRoles.Administrator);
        Version(1);
    }

    public override async Task HandleAsync(SearchEntityRequest req, CancellationToken ct)
    {
        
        var group = await _db.Groups.FindAsync(new object?[] { req.Id }, cancellationToken: ct);

        if (group is null)
        {
            ThrowError("Group was not found", 404);
        }
        
        _db.Groups.Remove(group);
        await _db.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
    }
}