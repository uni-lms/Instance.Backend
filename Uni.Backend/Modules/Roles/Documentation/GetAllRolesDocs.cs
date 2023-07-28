using FastEndpoints;
using Uni.Backend.Modules.Roles.Contract;
using Uni.Backend.Modules.Roles.Endpoints;

namespace Uni.Backend.Modules.Roles.Documentation;

public class GetAllRolesDocs : Summary<GetAllRoles>
{
    public GetAllRolesDocs()
    {
        Summary = "Gets all available roles";
        Response(200, "Roles fetched successfully",
            example: new List<Role> { new() { Id = Guid.NewGuid(), Name = "Tutors" } });
    }
}