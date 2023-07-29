using Uni.Backend.Modules.Groups.Contracts;
using Uni.Backend.Modules.Users.Contracts;

namespace Uni.Backend.Modules.Courses.Contracts;

public class CourseDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Abbreviation { get; set; }
    public IEnumerable<GroupDto>? AssignedGroups { get; set; }
    public int Semester { get; set; }
    public IEnumerable<UserDto>? Owners { get; set; }
}