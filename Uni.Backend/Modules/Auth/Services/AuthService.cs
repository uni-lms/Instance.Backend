using System.Security.Cryptography;
using System.Text;
using Uni.Backend.Modules.Auth.Contracts;
using Uni.Backend.Modules.Users.Contracts;

namespace Uni.Backend.Modules.Auth.Services;

public class AuthService
{
    public bool ValidateCredentials(User entity, LoginRequest req)
    {
        return VerifyPasswordHash(req.Password, entity.PasswordHash, entity.PasswordSalt);
    }

    public string GeneratePassword()
    {
        var passwordBytes = RandomNumberGenerator.GetBytes(13);
        return Convert.ToBase64String(passwordBytes);
    }

    private bool VerifyPasswordHash(string plainPassword, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(plainPassword));
        return computedHash.SequenceEqual(passwordHash);
    }

    public void CreatePasswordHash(string plainPassword, out byte[] passwordSalt, out byte[] passwordHash)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(plainPassword));
    }
}