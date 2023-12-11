using System.Net.Mime;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using Uni.Backend.Data;
using Uni.Backend.Modules.Auth.Contracts;
using Uni.Backend.Modules.Auth.Services;


namespace Uni.Backend.Modules.Auth.Endpoints;

public class RestorePassword : Endpoint<RestorePasswordRequest, EmptyResponse> {
  private readonly AppDbContext _db;
  private readonly AuthService _authService;

  public RestorePassword(AppDbContext db, AuthService authService) {
    _db = db;
    _authService = authService;
  }

  public override void Configure() {
    Version(1);
    Post("/auth/restore-password");
    AllowAnonymous();
    Options(x => x.WithTags("Auth"));
    Description(b => b
      .Produces<EmptyResponse>(200, MediaTypeNames.Application.Json)
      .ProducesProblemFE(404)
      .ProducesProblemFE(500));
    Summary(x => {
      x.Summary = "Changes password of current user";
      x.Description = "<b>Allowed scopes:</b> Any authorized user";
      x.Responses[200] = "Password successfully changed";
      x.Responses[404] = "User was not found";
      x.Responses[500] = "Some other error occured";
    });
  }

  public override async Task HandleAsync(RestorePasswordRequest req, CancellationToken ct) {
    var user = await _db.Users.Where(e => e.Email == req.Email).FirstOrDefaultAsync(ct);

    if (user is null) {
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