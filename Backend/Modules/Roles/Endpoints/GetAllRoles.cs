using System.Net.Mime;
using Backend.Data;
using Backend.Modules.Roles.Contract;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Backend.Modules.Roles.Endpoints;

public class GetAllRoles : EndpointWithoutRequest<List<Role>>
{
    private readonly AppDbContext _db;

    public GetAllRoles(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Get("/api/roles");
        AllowAnonymous();
        Description(b => b
            .Produces<List<Role>>(200, MediaTypeNames.Application.Json)
            .ProducesProblemFE<InternalErrorResponse>(500));
        Options(x => x.WithTags("Roles"));
        Version(1);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _db.Roles.ToListAsync(ct);

        await SendAsync(result, cancellation: ct);
    }
}