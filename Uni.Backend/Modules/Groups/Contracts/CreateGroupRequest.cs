using Uni.Backend.Modules.Users.Contracts;

namespace Uni.Backend.Modules.Groups.Contracts;

public class CreateGroupRequest
{
    public required string Name { get; set; }
    public int CurrentSemester { get; set; }
    public int MaxSemester { get; set; }
    public required List<CreateUserRequest> Users { get; set; }
}