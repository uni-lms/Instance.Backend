using FastEndpoints;
using Uni.Backend.Modules.Users.Endpoints;

namespace Uni.Backend.Modules.Users.Documentation;

public class GetAllUsersDocs: Summary<GetAllUsers>
{
    public GetAllUsersDocs()
    {
        Summary = "Gets all available users";
        Response(200, "Users fetched successfully");
    }
}