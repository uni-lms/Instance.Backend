using FastEndpoints;
using Uni.Backend.Modules.Groups.Endpoints;

namespace Uni.Backend.Modules.Groups.Documentation;

public class GetAllGroupsDocs: Summary<GetAllGroups>
{
    public GetAllGroupsDocs()
    {
        Summary = "Gets all available groups";
        Response(200, "Groups fetched successfully");
    }
}