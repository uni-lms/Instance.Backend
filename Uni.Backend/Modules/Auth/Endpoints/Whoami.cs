using System.Net.Mime;
using System.Security.Claims;
using FastEndpoints;
using Uni.Backend.Modules.Auth.Contracts;

namespace Uni.Backend.Modules.Auth.Endpoints;

public class Whoami : EndpointWithoutRequest<WhoamiResponse>
{
    public override void Configure()
    {
        Get("/auth/whoami");
        
        Options(x => x.WithTags("Auth"));
        Description(b => b
            .Produces<WhoamiResponse>(200, MediaTypeNames.Application.Json)
            .ProducesProblemFE<InternalErrorResponse>(500));
        Version(1);
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