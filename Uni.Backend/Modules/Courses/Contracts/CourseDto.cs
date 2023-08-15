using JetBrains.Annotations;
using Uni.Backend.Modules.Groups.Contracts;
using Uni.Backend.Modules.Users.Contracts;

namespace Uni.Backend.Modules.Courses.Contracts;

public class CourseDto
{
    public Guid Id { [UsedImplicitly] get; set; }
    public required string Name { [UsedImplicitly] get; set; }
    public required string Abbreviation { [UsedImplicitly] get; set; }
    public List<GroupDto>? AssignedGroups { [UsedImplicitly] get; set; }
    public int Semester { [UsedImplicitly] get; set; }
    public List<UserDto>? Owners { [UsedImplicitly] get; set; }
}