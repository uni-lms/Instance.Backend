using Backend.Modules.Groups.Endpoints;
using FastEndpoints;

namespace Backend.Modules.Groups.Documentation;

public class CreateGroupDocs : Summary<CreateGroup>
{
    public CreateGroupDocs()
    {
        Summary = "Creates new group, adding users to it and sending emails with credentials to them";
        Response(201, "Group created successfully");
        Response(404, "Role was not found");
    }
}