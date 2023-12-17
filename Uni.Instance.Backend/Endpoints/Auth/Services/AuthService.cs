using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using Ardalis.Result;

using FastEndpoints.Security;

using FluentValidation.Results;

using Microsoft.EntityFrameworkCore;

using Uni.Instance.Backend.Configuration;
using Uni.Instance.Backend.Data;
using Uni.Instance.Backend.Data.Models;
using Uni.Instance.Backend.Endpoints.Auth.Data;
using Uni.Instance.Backend.Extensions;

using LoginRequest = Uni.Instance.Backend.Endpoints.Auth.Data.LoginRequest;


namespace Uni.Instance.Backend.Endpoints.Auth.Services;

public class AuthService(AppDbContext db, SecurityConfiguration configuration) {
  public async Task<Result<LoginResponse>> SignUpAsync(
    bool validationFailed,
    IEnumerable<ValidationFailure> validationFailures,
    SignUpRequest req,
    CancellationToken cancellation = default
  ) {
    if (validationFailed) {
      return Result.Invalid(validationFailures.ToValidationErrors());
    }

    var hasUser = await db.Users.AnyAsync(e => e.Email == req.Email, cancellation);

    if (hasUser) {
      return Result.Conflict("User with this email already registered");
    }

    CreatePasswordHash(req.Password, out var salt, out var hash);

    var role = await db.Roles.FirstOrDefaultAsync(e => e.Id == req.RoleId, cancellation);

    if (role is null) {
      return Result.NotFound("Role was not found");
    }

    var user = new User {
      Email = req.Email,
      FirstName = req.FirstName,
      LastName = req.LastName,
      Patronymic = req.Patronymic,
      PasswordHash = hash,
      PasswordSalt = salt,
      Role = role,
    };

    await db.Users.AddAsync(user, cancellation);
    await db.SaveChangesAsync(cancellation);

    var response = new LoginResponse {
      AccessToken = GenerateAccessToken(user),
    };

    return Result.Success(response);
  }

  public async Task<Result<WhoamiResponse>> WhoamiAsync(ClaimsPrincipal user) {
    var email = user.ClaimValue(ClaimTypes.Name);
    var data = await db.Users.AsNoTracking().FirstOrDefaultAsync(e => e.Email == email);

    if (data is null) {
      return Result.NotFound("User was not found");
    }

    var response = new WhoamiResponse {
      Email = email!,
      FullName = $"{data.FirstName}",
      RoleName = user.ClaimValue(ClaimTypes.Role)!,
    };

    return Result.Success(response);
  }

  public async Task<Result<LoginResponse>> LoginAsync(
    bool validationFailed,
    IEnumerable<ValidationFailure> validationFailures,
    LoginRequest req,
    CancellationToken cancellation = default
  ) {
    if (validationFailed) {
      return Result.Invalid(validationFailures.ToValidationErrors());
    }

    var user = await db.Users
      .AsNoTracking()
      .Where(u => u.Email == req.Email)
      .Include(u => u.Role)
      .FirstOrDefaultAsync(cancellation);

    if (user is null) {
      return Result.NotFound("User was not found");
    }

    if (!IsValidPassword(user, req)) {
      return Result.Unauthorized();
    }

    var token = GenerateAccessToken(user);
    return Result.Success(new LoginResponse {
      AccessToken = token,
    });
  }

  private bool IsValidPassword(User entity, LoginRequest req) {
    return VerifyPasswordHash(req.Password, entity.PasswordHash, entity.PasswordSalt);
  }

  private bool VerifyPasswordHash(string plainPassword, byte[] passwordHash, byte[] passwordSalt) {
    using var hmac = new HMACSHA512(passwordSalt);
    var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(plainPassword));
    return computedHash.SequenceEqual(passwordHash);
  }

  private void CreatePasswordHash(string plainPassword, out byte[] passwordSalt, out byte[] passwordHash) {
    using var hmac = new HMACSHA512();
    passwordSalt = hmac.Key;
    passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(plainPassword));
  }

  private string GenerateAccessToken(User user) {
    return JWTBearer.CreateToken(
      signingKey: configuration.SigningKey,
      expireAt: DateTime.UtcNow.AddMinutes(40),
      claims: new[] { new Claim(ClaimTypes.Name, user.Email) }
    );
  }

  public async Task<string> GetRoleOfUser(string email) {
    var user = await db.Users.Where(e => e.Email == email).Include(e => e.Role).FirstOrDefaultAsync();

    return user is null ? string.Empty : user.Role.Name;
  }
}