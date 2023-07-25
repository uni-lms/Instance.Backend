using Backend.Modules.Users.Contract;

namespace Backend.Modules.Groups.Contract;

public class CreateGroupDto
{
    public required GroupDto Group { get; set; }
    public required IEnumerable<UserCredentials> UsersData { get; set; }
}