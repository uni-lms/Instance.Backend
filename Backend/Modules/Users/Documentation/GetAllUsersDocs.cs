using Backend.Modules.Users.Endpoints;
using FastEndpoints;

namespace Backend.Modules.Users.Documentation;

public class GetAllUsersDocs: Summary<GetAllUsers>
{
    public GetAllUsersDocs()
    {
        Summary = "Gets all available users";
        Response(200, "Users fetched successfully");
    }
}