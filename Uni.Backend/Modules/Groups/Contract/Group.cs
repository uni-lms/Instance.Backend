using System.Collections;
using Uni.Backend.Data;
using Uni.Backend.Modules.Courses.Contract;
using Uni.Backend.Modules.Users.Contract;

namespace Uni.Backend.Modules.Groups.Contract;

public class Group : BaseModel
{
    public required string Name { get; set; }
    public int CurrentSemester { get; set; }
    public int MaxSemester { get; set; }
    public required IEnumerable<User> Students { get; set; }
    public IEnumerable<Course>? Courses { get; set; }
}