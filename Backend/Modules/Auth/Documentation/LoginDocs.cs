using Backend.Modules.Auth.Endpoints;
using FastEndpoints;

namespace Backend.Modules.Auth.Documentation;

public class LoginDocs : Summary<Login>
{
    public LoginDocs()
    {
        Summary = "Get JWT token for user with specified email and password";
        Response(200, "Token fetched successfully");
        Response(401, "Wrong password passed");
        Response(404, "User with specified email was not found");
    }
}