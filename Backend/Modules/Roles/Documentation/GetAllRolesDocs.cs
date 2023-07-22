using Backend.Modules.Roles.Contract;
using Backend.Modules.Roles.Endpoints;
using FastEndpoints;

namespace Backend.Modules.Roles.Documentation;

public class GetAllRolesDocs : Summary<GetAllRoles>
{
    public GetAllRolesDocs()
    {
        Summary = "Gets all available roles";
        Response(200, "Roles fetched successfully",
            example: new List<Role> { new() { Id = Guid.NewGuid(), Name = "Tutors" } });
    }
}