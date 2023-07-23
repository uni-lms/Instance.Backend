using System.Security.Cryptography;
using System.Text;
using Backend.Modules.Auth.Contracts;
using Backend.Modules.Users.Contract;

namespace Backend.Modules.Auth.Services;

public class AuthService
{
    public bool ValidateCredentials(User entity, LoginRequest req)
    {
        return VerifyPasswordHash(req.Password, entity.PasswordHash, entity.PasswordSalt);
    }

    private bool VerifyPasswordHash(string plainPassword, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(plainPassword));
        return computedHash.SequenceEqual(passwordHash);
    }
}