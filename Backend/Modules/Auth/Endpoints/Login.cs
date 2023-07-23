using System.Net.Mime;
using System.Security.Claims;
using Backend.Configuration;
using Backend.Data;
using Backend.Modules.Auth.Contracts;
using Backend.Modules.Auth.Services;
using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.EntityFrameworkCore;

namespace Backend.Modules.Auth.Endpoints;

public class Login : Endpoint<LoginRequest, LoginResponse>
{
    private readonly AppDbContext _db;
    private readonly AuthService _authService;
    private readonly SecurityConfiguration _securityOptions;

    public Login(SecurityConfiguration securityOptions, AppDbContext db, AuthService authService)
    {
        _db = db;
        _authService = authService;
        _securityOptions = securityOptions;
    }

    public override void Configure()
    {
        Post("/auth/login");
        AllowAnonymous();
        Description(b => b
            .Produces<LoginResponse>(200, MediaTypeNames.Application.Json)
            .ProducesProblemFE<ValidationFailureException>(401)
            .ProducesProblemFE<ValidationFailureException>(404, "")
            .ProducesProblemFE<InternalErrorResponse>(500));
        Options(x => x.WithTags("Auth"));
        Version(1);
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        var user = await _db.Users
            .Where(u => u.Email == req.Email)
            .Include(u => u.Role)
            .FirstOrDefaultAsync(ct);

        if (user is not null)
        {
            if (_authService.ValidateCredentials(user, req))
            {
                var jwtToken = JWTBearer.CreateToken(
                    signingKey: _securityOptions.SigningKey,
                    expireAt: DateTime.UtcNow.AddMinutes(15),
                    priviledges: u =>
                    {
                        u.Roles.Add(user.Role.Name);
                        u.Claims.Add(new Claim(ClaimTypes.Name, user.Email));
                    }
                );

                await SendAsync(new LoginResponse
                {
                    Email = req.Email,
                    Token = jwtToken
                }, cancellation: ct);
            }
            else
            {
                ThrowError("Wrong password", 401);
            }
        }
        else
        {
            ThrowError("User was not found", 404);
        }
    }
}