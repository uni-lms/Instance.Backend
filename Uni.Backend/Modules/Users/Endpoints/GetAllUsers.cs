using System.Net.Mime;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Uni.Backend.Configuration;
using Uni.Backend.Data;
using Uni.Backend.Modules.Users.Contracts;

namespace Uni.Backend.Modules.Users.Endpoints;

public class GetAllUsers : EndpointWithoutRequest<List<UserDto>, UserMapper>
{
    private readonly AppDbContext _db;

    public GetAllUsers(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Version(1);
        Get("/users");
        Roles(UserRoles.Administrator);
        Options(x => x.WithTags("Users"));
        Description(b => b
            .Produces<List<UserDto>>(200, MediaTypeNames.Application.Json)
            .ProducesProblemFE(401)
            .ProducesProblemFE(403)
            .ProducesProblemFE(500));
        Summary(x =>
        {
            x.Summary = "Gets all users";
            x.Description = """
                               <b>Allowed scopes:</b> Administrator
                            """;
            x.Responses[200] = "List of users fetched successfully";
            x.Responses[401] = "Not authorized";
            x.Responses[403] = "Access forbidden";
            x.Responses[500] = "Some other error occured";
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _db.Users.AsNoTracking()
            .Include(e => e.Role)
            .Select(e => Map.FromEntity(e)).ToListAsync(ct);
        await SendAsync(result, cancellation: ct);
    }
}