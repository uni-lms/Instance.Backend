using FastEndpoints;
using Uni.Backend.Modules.Groups.Contracts;
using Uni.Backend.Modules.Users.Contracts;

namespace Uni.Backend.Modules.Courses.Contracts;

public class CoursesMapper : ResponseMapper<CourseDto, Course>
{
    public override CourseDto FromEntity(Course r)
    {
        var groupMapper = new GroupMapper();
        var userMapper = new UserMapper();
        return new CourseDto
        {
            Id = r.Id,
            Name = r.Name,
            Abbreviation = r.Abbreviation,
            AssignedGroups = r.AssignedGroups?
                .Select(g => groupMapper.FromEntity(g)).ToList(),
            Owners = r.Owners.Select(e => userMapper.FromEntity(e)).ToList(),
            Semester = r.Semester
        };
    }
}