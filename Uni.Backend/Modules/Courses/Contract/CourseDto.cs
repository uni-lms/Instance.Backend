using Uni.Backend.Modules.Groups.Contract;
using Uni.Backend.Modules.Users.Contract;

namespace Uni.Backend.Modules.Courses.Contract;

public class CourseDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Abbreviation { get; set; }
    public IEnumerable<GroupDto>? AssignedGroups { get; set; }
    public int Semester { get; set; }
    public IEnumerable<UserDto>? Owners { get; set; }
}