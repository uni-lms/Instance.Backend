using System.Net.Mime;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Uni.Backend.Data;
using Uni.Backend.Modules.Roles.Contracts;

namespace Uni.Backend.Modules.Roles.Endpoints;

public class GetAllRoles : EndpointWithoutRequest<List<Role>>
{
    private readonly AppDbContext _db;

    public GetAllRoles(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Version(1);
        Get("/roles");
        Options(x => x.WithTags("Roles"));
        Description(b => b
            .Produces<List<Role>>(200, MediaTypeNames.Application.Json)
            .ProducesProblemFE(401)
            .ProducesProblemFE(500));
        Summary(x =>
        {
            x.Summary = "Gets all available user roles";
            x.Description = """
                               <b>Allowed scopes:</b> Any authorized user
                            """;
            x.Responses[200] = "List of roles fetched successfully";
            x.Responses[200] = "Unauthorized";
            x.Responses[500] = "Some other error occured";
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _db.Roles.ToListAsync(ct);

        await SendAsync(result, cancellation: ct);
    }
}