using System.Net.Mime;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using Uni.Backend.Data;
using Uni.Backend.Modules.Auth.Contracts;
using Uni.Backend.Modules.Auth.Services;
using Uni.Backend.Modules.Users.Contracts;


namespace Uni.Backend.Modules.Auth.Endpoints;

public class Signup : Endpoint<SignupRequest, SignupResponse> {
  private readonly AppDbContext _db;
  private readonly AuthService _authService;

  public Signup(AppDbContext db, AuthService authService) {
    _db = db;
    _authService = authService;
  }

  public override void Configure() {
    Version(1);
    Post("/auth/signup");
    AllowAnonymous();
    Options(x => x.WithTags("Auth"));
    Description(b => b
      .Produces<SignupResponse>(201, MediaTypeNames.Application.Json)
      .ProducesProblemFE(404)
      .ProducesProblemFE(409)
      .ProducesProblemFE(500));
    Summary(x => {
      x.Summary = "Changes password of current user";
      x.Description = "<b>Allowed scopes:</b> Anyone<br/><b>Date format:</b> yyyy-MM-dd";
      x.Responses[201] = "User was successfully registered";
      x.Responses[404] = "Some related entity was not found";
      x.Responses[409] = "User with this email has already been registered";
      x.Responses[500] = "Some other error occured";
    });
  }

  public override async Task HandleAsync(SignupRequest req, CancellationToken ct) {
    var existingUser = await _db.Users.Where(e => e.Email == req.Email).FirstOrDefaultAsync(ct);

    if (existingUser is not null) {
      ThrowError("User with this email already registered", 409);
    }

    var password = _authService.GeneratePassword();
    _authService.CreatePasswordHash(password, out var passwordSalt, out var passwordHash);
    var role = await _db.Roles.FindAsync(new object?[] { req.Role }, cancellationToken: ct);
    var gender = await _db.Genders.FindAsync(new object?[] { req.Gender }, cancellationToken: ct);
    var avatar = await _db.StaticFiles.FindAsync(
      new object?[] { req.Avatar },
      cancellationToken: ct
    );

    if (role is null) {
      ThrowError(e => e.Role, "Role was not found", 404);
    }

    if (gender is null) {
      ThrowError(e => e.Gender, "Gender was not found", 404);
    }

    var user = new User {
      Email = req.Email,
      FirstName = req.FirstName,
      LastName = req.LastName,
      Patronymic = req.Patronymic,
      DateOfBirth = req.DateOfBirth,
      PasswordHash = passwordHash,
      PasswordSalt = passwordSalt,
      Role = role,
      Gender = gender,
      Avatar = avatar,
    };

    await _db.Users.AddAsync(user, ct);
    await _db.SaveChangesAsync(ct);

    var result = new SignupResponse {
      FirstName = req.FirstName,
      LastName = req.LastName,
      Email = req.Email,
      Password = password,
    };

    await SendCreatedAtAsync("/auth/register", null, result, cancellation: ct);
  }
}