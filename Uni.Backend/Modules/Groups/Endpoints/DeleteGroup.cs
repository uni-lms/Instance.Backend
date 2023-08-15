using System.Net.Mime;
using FastEndpoints;
using Uni.Backend.Configuration;
using Uni.Backend.Data;
using Uni.Backend.Modules.Common.Contracts;

namespace Uni.Backend.Modules.Groups.Endpoints;

public class DeleteGroup: Endpoint<SearchEntityRequest>
{

    private readonly AppDbContext _db;

    public DeleteGroup(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Version(1);
        Delete("/groups");
        Roles(UserRoles.Administrator);
        Options(x => x.WithTags("Groups"));
        Description(b => b
            .Produces<EmptyResponse>(204, MediaTypeNames.Application.Json)
            .ProducesProblemFE(401)
            .ProducesProblemFE(403)
            .ProducesProblemFE(404)
            .ProducesProblemFE(500));
        Summary(x =>
        {
            x.Summary = "Permanently deletes group";
            x.Description = "<b>Allowed scopes:</b> Administrator";
            x.Responses[204] = "Group deleted";
            x.Responses[401] = "Not authorized";
            x.Responses[403] = "Access forbidden";
            x.Responses[404] = "Group was not found";
            x.Responses[500] = "Some other error occured";
        });
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