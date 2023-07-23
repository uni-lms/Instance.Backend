using Backend.Modules.Groups.Contract;
using Backend.Modules.Users.Contract;

namespace Backend.Modules.Courses.Contract;

public class CourseDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Abbreviation { get; set; }
    public required IEnumerable<GroupDto> AssignedGroups { get; set; }
    public int Semester { get; set; }
    public required UserDto Owner { get; set; }
}