using System.Net.Mime;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Uni.Backend.Configuration;
using Uni.Backend.Data;
using Uni.Backend.Modules.Groups.Contract;

namespace Uni.Backend.Modules.Groups.Endpoints;

public class GetAllGroups : EndpointWithoutRequest<List<GroupDto>, GroupMapper>
{
    private readonly AppDbContext _db;

    public GetAllGroups(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Get("/groups");
        Description(b => b
            .Produces<List<GroupDto>>(200, MediaTypeNames.Application.Json)
            .ProducesProblemFE<InternalErrorResponse>(500));
        Options(x => x.WithTags("Groups"));
        Version(1);
        Roles(UserRoles.Tutor, UserRoles.Administrator);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _db.Groups.Select(e => Map.FromEntity(e)).ToListAsync(ct);
        await SendAsync(result, cancellation: ct);
    }
}