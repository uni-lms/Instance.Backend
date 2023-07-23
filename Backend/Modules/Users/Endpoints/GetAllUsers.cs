using System.Net.Mime;
using Backend.Data;
using Backend.Modules.Users.Contract;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Backend.Modules.Users.Endpoints;

public class GetAllUsers: EndpointWithoutRequest<List<UserDto>, UserMapper>
{

    private readonly AppDbContext _db;

    public GetAllUsers(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Get("/users");
        AllowAnonymous();
        Description(b => b
            .Produces<List<UserDto>>(200, MediaTypeNames.Application.Json)
            .ProducesProblemFE<InternalErrorResponse>(500));
        Options(x => x.WithTags("Users"));
        Version(1);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _db.Users.Select(e => Map.FromEntity(e)).ToListAsync(ct);
        await SendAsync(result, cancellation: ct);
    }
}