using System.Net.Mime;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Uni.Backend.Configuration;
using Uni.Backend.Data;
using Uni.Backend.Modules.Groups.Contracts;

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
        Version(1);
        Get("/groups");
        Roles(UserRoles.MinimumRequired(UserRoles.Tutor));
        Options(x => x.WithTags("Groups"));
        Description(b => b
            .Produces<CreateGroupDto>(200, MediaTypeNames.Application.Json)
            .ProducesProblemFE(401)
            .ProducesProblemFE(403)
            .ProducesProblemFE(500));
        Summary(x =>
        {
            x.Summary = "Gets list of all groups";
            x.Description = """
                               <b>Allowed scopes:</b> Tutor, Administrator
                            """;
            x.Responses[200] = "Successfully fetched list of groups";
            x.Responses[401] = "Not authorized";
            x.Responses[403] = "Access forbidden";
            x.Responses[500] = "Some other error occured";
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _db.Groups.AsNoTracking().Select(e => Map.FromEntity(e)).ToListAsync(ct);
        await SendAsync(result, cancellation: ct);
    }
}