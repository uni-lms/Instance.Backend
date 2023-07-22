using Backend.Data;
using Backend.Modules.Users.Contract;

namespace Backend.Modules.Groups.Contract;

public class Group: BaseModel
{
    public string Name { get; set; }
    public IEnumerable<User> Students { get; set; }
}