using Backend.Modules.Auth.Endpoints;
using FastEndpoints;

namespace Backend.Modules.Auth.Documentation;

public class SignupDocs: Summary<Signup>
{
    public SignupDocs()
    {
        Summary = "Registers user";
        Response(201, "User was created successfully");
    }
}