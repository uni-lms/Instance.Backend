using Backend.Modules.Groups.Endpoints;
using FastEndpoints;

namespace Backend.Modules.Groups.Documentation;

public class GetAllGroupsDocs: Summary<GetAllGroups>
{
    public GetAllGroupsDocs()
    {
        Summary = "Gets all available groups";
        Response(200, "Groups fetched successfully");
    }
}