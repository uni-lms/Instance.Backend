using System.Net.Mime;
using System.Security.Claims;

using FastEndpoints;
using FastEndpoints.Security;

using Microsoft.EntityFrameworkCore;

using Uni.Backend.Configuration;
using Uni.Backend.Data;
using Uni.Backend.Modules.Auth.Contracts;
using Uni.Backend.Modules.Auth.Services;


namespace Uni.Backend.Modules.Auth.Endpoints;

public class Login : Endpoint<LoginRequest, LoginResponse> {
  private readonly AppDbContext _db;
  private readonly AuthService _authService;
  private readonly SecurityConfiguration _securityOptions;

  public Login(SecurityConfiguration securityOptions, AppDbContext db, AuthService authService) {
    _db = db;
    _authService = authService;
    _securityOptions = securityOptions;
  }

  public override void Configure() {
    Version(1);
    Post("/auth/login");
    AllowAnonymous();
    Options(x => x.WithTags("Auth"));
    Description(b => b
      .Produces<LoginResponse>(200, MediaTypeNames.Application.Json)
      .ProducesProblemFE(401)
      .ProducesProblemFE(404)
      .ProducesProblemFE(500));
    Summary(x => {
      x.Summary = "Generates auth token for user by login and password";
      x.Description = "<b>Allowed scopes:</b> Anyone";
      x.Responses[200] = "Token successfully generated";
      x.Responses[401] = "Wrong password provided";
      x.Responses[404] = "User was not found";
      x.Responses[500] = "Some other error occured";
    });
  }

  public override async Task HandleAsync(LoginRequest req, CancellationToken ct) {
    var user = await _db.Users
      .AsNoTracking()
      .Where(u => u.Email == req.Email)
      .Include(u => u.Role)
      .FirstOrDefaultAsync(ct);

    if (user is not null) {
      if (_authService.ValidateCredentials(user, req)) {
        var jwtToken = JWTBearer.CreateToken(
          signingKey: _securityOptions.SigningKey,
          expireAt: DateTime.UtcNow.AddMonths(99),
          roles: new[] { user.Role!.Name },
          claims: new[] { (ClaimTypes.Name, user.Email) }
        );

        await SendAsync(new LoginResponse {
          Email = req.Email,
          Token = jwtToken,
        }, cancellation: ct);
      }
      else {
        ThrowError(e => e.Password, "Wrong password", 401);
      }
    }
    else {
      ThrowError(e => e.Email, "User was not found", 404);
    }
  }
}