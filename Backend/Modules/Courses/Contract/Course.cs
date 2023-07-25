using Backend.Data;
using Backend.Modules.CourseBlocks.Contracts;
using Backend.Modules.Groups.Contract;
using Backend.Modules.Users.Contract;

namespace Backend.Modules.Courses.Contract;

public class Course: BaseModel
{
    public required string Name { get; set; }
    public required string Abbreviation { get; set; }
    public required IEnumerable<Group> AssignedGroups { get; set; }
    public int Semester { get; set; }
    public required IEnumerable<User> Owners { get; set; }
    public required IEnumerable<CourseBlock> Blocks { get; set; }
}