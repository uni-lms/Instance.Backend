using System.Net.Mime;
using Backend.Data;
using Backend.Modules.Groups.Contract;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Backend.Modules.Groups.Endpoints;

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
        Roles("Administrator");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var groupMapper = Resolve<GroupMapper>();
        var result = await _db.Groups.Select(e => groupMapper.FromEntity(e)).ToListAsync(ct);
        await SendAsync(result, cancellation: ct);
    }
}