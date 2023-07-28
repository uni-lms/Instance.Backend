using System.Net.Mime;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Uni.Backend.Configuration;
using Uni.Backend.Data;
using Uni.Backend.Modules.Users.Contract;

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
        Get("/users");
        Description(b => b
            .Produces<List<UserDto>>(200, MediaTypeNames.Application.Json)
            .ProducesProblemFE<InternalErrorResponse>(500));
        Options(x => x.WithTags("Users"));
        Roles(UserRoles.Administrator);
        Version(1);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _db.Users
            .Include(e => e.Role)
            .Select(e => Map.FromEntity(e)).ToListAsync(ct);
        await SendAsync(result, cancellation: ct);
    }
}