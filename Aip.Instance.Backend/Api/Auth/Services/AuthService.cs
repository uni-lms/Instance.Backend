using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using Aip.Instance.Backend.Api.Auth.Data;
using Aip.Instance.Backend.Configuration.Models;
using Aip.Instance.Backend.Data;
using Aip.Instance.Backend.Data.Models;
using Aip.Instance.Backend.Extensions;

using Ardalis.Result;

using FastEndpoints.Security;

using FluentValidation.Results;

using Microsoft.EntityFrameworkCore;


namespace Aip.Instance.Backend.Api.Auth.Services;

public class AuthService(AppDbContext db, SecurityConfiguration configuration) {
  public async Task<Result<LogInResponse>> SignUpAsync(
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
      return Result.Conflict("Пользователь с этим адресом почты уже был зарегистрирован");
    }

    CreatePasswordHash(req.Password, out var salt, out var hash);

    var role = await db.Roles.FirstOrDefaultAsync(e => e.Id == req.RoleId, cancellation);

    if (role is null) {
      return Result.NotFound("Нет такой роли");
    }

    var user = new User {
      Email = req.Email,
      FirstName = req.FirstName,
      LastName = req.LastName,
      Patronymic = req.Patronymic,
      PasswordHash = hash,
      PasswordSalt = salt,
    };

    await db.Users.AddAsync(user, cancellation);

    var response = new LogInResponse {
      AccessToken = GenerateAccessToken(user),
    };

    if (!string.IsNullOrEmpty(req.FcmToken)) {
      await SaveMobileSession(req.FcmToken, user, cancellation);
    }

    await db.SaveChangesAsync(cancellation);

    return Result.Success(response);
  }

  public async Task<Result<WhoamiResponse>> WhoamiAsync(ClaimsPrincipal user) {
    var email = user.ClaimValue(ClaimTypes.Name);
    var data = await db.Users.AsNoTracking().FirstOrDefaultAsync(e => e.Email == email);

    if (data is null) {
      return Result.NotFound("Пользователь не был найден");
    }

    var fullNameBuilder = new StringBuilder();
    fullNameBuilder.Append(data.LastName);
    fullNameBuilder.Append(' ');
    fullNameBuilder.Append(data.FirstName[0]);
    fullNameBuilder.Append('.');

    if (data.Patronymic is not null) {
      fullNameBuilder.Append(' ');
      fullNameBuilder.Append(data.Patronymic[0]);
      fullNameBuilder.Append('.');
    }

    var response = new WhoamiResponse {
      Email = email!,
      FullName = fullNameBuilder.ToString(),
    };

    return Result.Success(response);
  }

  public async Task<Result<LogInResponse>> LoginAsync(
    bool validationFailed,
    IEnumerable<ValidationFailure> validationFailures,
    LogInRequest req,
    CancellationToken cancellation = default
  ) {
    if (validationFailed) {
      return Result.Invalid(validationFailures.ToValidationErrors());
    }

    var user = await db.Users
      .AsNoTracking()
      .Where(u => u.Email == req.Email)
      .FirstOrDefaultAsync(cancellation);

    if (user is null) {
      return Result.NotFound("Пользователь не был найден");
    }

    if (!IsValidPassword(user, req)) {
      return Result.Unauthorized();
    }

    var token = GenerateAccessToken(user);

    // if (!string.IsNullOrEmpty(req.FcmToken)) {
    //   await SaveMobileSession(req.FcmToken, user, cancellation);
    //   await db.SaveChangesAsync(cancellation);
    // }

    return Result.Success(new LogInResponse {
      AccessToken = token,
    });
  }

  private bool IsValidPassword(User entity, LogInRequest req) {
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

  private async Task SaveMobileSession(string token, User user, CancellationToken ct = default) {
    var session = await db.MobileSessions.FirstOrDefaultAsync(e => e.User == user, ct);

    if (session is not null) {
      session.FcmToken = token;
      return;
    }

    await db.MobileSessions.AddAsync(new MobileSession {
      FcmToken = token,
      User = user,
    }, ct);
  }

  public async Task<Result<DeleteUserResponse>> DeleteAccountAsync(string email) {
    var user = await db.Users.Where(e => e.Email == email).FirstOrDefaultAsync();

    if (user is null) {
      return Result<DeleteUserResponse>.NotFound("Пользователь не был найден");
    }

    db.Users.Remove(user);
    await db.SaveChangesAsync();

    return Result.Success(new DeleteUserResponse {
      Email = user.Email,
    });
  }
}