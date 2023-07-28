using System.Net.Mime;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Uni.Backend.Data;
using Uni.Backend.Modules.Roles.Contract;

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
        Get("/roles");
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