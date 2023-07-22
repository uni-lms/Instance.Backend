using Backend.Data;
using Backend.Modules.Users.Contract;

namespace Backend.Modules.Courses.Contract;

public class Course: BaseModel
{
    public string Name { get; set; }
    public string Abbreviation { get; set; }
    public IEnumerable<Group> AssignedGroups { get; set; }
    public int Semester { get; set; }
    public User Owner { get; set; }
}