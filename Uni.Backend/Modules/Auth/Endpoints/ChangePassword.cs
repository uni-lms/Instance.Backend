using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Uni.Backend.Data;
using Uni.Backend.Modules.Auth.Contracts;
using Uni.Backend.Modules.Auth.Services;

namespace Uni.Backend.Modules.Auth.Endpoints;

public class ChangePassword : Endpoint<ChangePasswordRequest, EmptyResponse>
{
    private readonly AppDbContext _db;
    private readonly AuthService _authService;

    public ChangePassword(AppDbContext db, AuthService authService)
    {
        _db = db;
        _authService = authService;
    }

    public override void Configure()
    {
        Post("/auth/change-password");
        Options(x => x.WithTags("Auth"));
        Version(1);
    }

    public override async Task HandleAsync(ChangePasswordRequest req, CancellationToken ct)
    {
        var user = await _db.Users.Where(e => e.Email == User.Identity!.Name).FirstOrDefaultAsync(ct);

        if (user is null)
        {
            ThrowError(
                e => User.Identity!.Name!,
                $"User with email {User.Identity!.Name} is not found",
                404
            );
        }

        if (!_authService.ValidateCredentials(user, new LoginRequest
            {
                Email = "",
                Password = req.OldPassword
            }))
        {
            ThrowError("Wrong password", 403);
        }

        _authService.CreatePasswordHash(req.NewPassword, out var passwordSalt, out var passwordHash);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        await _db.SaveChangesAsync(ct);

        await SendAsync(new EmptyResponse(), cancellation: ct);
    }
}