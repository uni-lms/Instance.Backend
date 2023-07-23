using Backend.Modules.Users.Contract;

namespace Backend.Modules.Groups.Contract;

public class CreateGroupRequest
{
    public required string Name { get; set; }
    public int CurrentSemester { get; set; }
    public int MaxSemester { get; set; }
    public required IEnumerable<CreateUserRequest> Users { get; set; }
}