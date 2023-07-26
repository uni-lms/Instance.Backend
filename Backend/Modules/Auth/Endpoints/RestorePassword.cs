using Backend.Data;
using Backend.Modules.Auth.Contracts;
using Backend.Modules.Auth.Services;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Backend.Modules.Auth.Endpoints;

public class RestorePassword : Endpoint<RestorePasswordRequest, EmptyResponse>
{
    private readonly AppDbContext _db;
    private readonly AuthService _authService;

    public RestorePassword(AppDbContext db, AuthService authService)
    {
        _db = db;
        _authService = authService;
    }

    public override void Configure()
    {
        Post("/auth/restore-password");
        AllowAnonymous();
        Options(x => x.WithTags("Auth"));
        Version(1);
    }

    public override async Task HandleAsync(RestorePasswordRequest req, CancellationToken ct)
    {
        var user = await _db.Users.Where(e => e.Email == req.Email).FirstOrDefaultAsync(ct);

        if (user is null)
        {
            ThrowError(
                e => e.Email,
                $"User with email {req.Email} is not found",
                404
            );
        }

        _authService.CreatePasswordHash(req.NewPassword, out var passwordSalt, out var passwordHash);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        await _db.SaveChangesAsync(ct);

        await SendAsync(new EmptyResponse(), cancellation: ct);
    }
}