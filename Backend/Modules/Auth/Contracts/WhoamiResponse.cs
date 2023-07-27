using System.Security.Claims;

namespace Backend.Modules.Auth.Contracts;

public class WhoamiResponse
{
    public required string Email { get; set; }
    public required string Role { get; set; }
}