using Backend.Modules.Auth.Endpoints;
using FastEndpoints;

namespace Backend.Modules.Auth.Documentation;

public class WhoamiDocs: Summary<Whoami>
{
    public WhoamiDocs()
    {
        Summary = "Gets information about current user";
        Response(200, "Information fetched successfully");
    }
}