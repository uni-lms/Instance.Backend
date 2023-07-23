using System.Security.Claims;

namespace Backend.Modules.Auth.Contracts;

public class WhoamiResponse
{
    public string Email { get; set; }
    public string Role { get; set; }
}