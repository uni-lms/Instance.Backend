using System.Security.Claims;
using Backend.Modules.Auth.Contracts;
using FastEndpoints;

namespace Backend.Modules.Auth.Endpoints;

public class Whoami : EndpointWithoutRequest<WhoamiResponse>
{
    public override void Configure()
    {
        Get("/auth/whoami");
        Options(x => x.WithTags("Auth"));
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = new WhoamiResponse
        {
            Email = User.Identity?.Name ?? "",
            Role = User.FindAll(ClaimTypes.Role).ToList()[0].Value
        };
        await SendAsync(result, cancellation: ct);
    }
}