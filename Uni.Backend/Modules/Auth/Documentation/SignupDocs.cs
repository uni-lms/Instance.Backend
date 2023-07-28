using FastEndpoints;
using Uni.Backend.Modules.Auth.Endpoints;

namespace Uni.Backend.Modules.Auth.Documentation;

public class SignupDocs: Summary<Signup>
{
    public SignupDocs()
    {
        Summary = "Registers user";
        Response(201, "User was created successfully");
    }
}