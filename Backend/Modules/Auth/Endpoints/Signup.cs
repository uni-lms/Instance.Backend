using System.Net.Mime;
using Backend.Data;
using Backend.Modules.Auth.Contracts;
using Backend.Modules.Auth.Services;
using Backend.Modules.Users.Contract;
using FastEndpoints;

namespace Backend.Modules.Auth.Endpoints;

public class Signup: Endpoint<SignupRequest, SignupResponse>
{

    private readonly AppDbContext _db;
    private readonly AuthService _authService;

    public Signup(AppDbContext db, AuthService authService)
    {
        _db = db;
        _authService = authService;
    }

    public override void Configure()
    {
        Post("/auth/signup");
        AllowAnonymous();
        Description(b => b
            .Produces<SignupResponse>(201, MediaTypeNames.Application.Json)
            .ProducesProblemFE<InternalErrorResponse>(500));
        Options(x => x.WithTags("Auth"));
        Version(1);
    }

    public override async Task HandleAsync(SignupRequest req, CancellationToken ct)
    {
        var password = _authService.GeneratePassword();
        _authService.CreatePasswordHash(password, out var passwordSalt, out var passwordHash);
        var role = await _db.Roles.FindAsync(new object?[] { req.Role }, cancellationToken: ct);

        if (role is null)
        {
            ThrowError("", 404);
        }
        
        var user = new User
        {
            Email = req.Email,
            FirstName = req.FirstName,
            LastName = req.LastName,
            Patronymic = req.Patronymic,
            DateOfBirth = req.DateOfBirth,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Role = role
        };

        await _db.Users.AddAsync(user, ct);
        await _db.SaveChangesAsync(ct);

        var result = new SignupResponse
        {
            FirstName = req.FirstName,
            LastName = req.LastName,
            Email = req.Email,
            Password = password
        };

        await SendCreatedAtAsync("/auth/register",null, result, cancellation: ct);
    }
}