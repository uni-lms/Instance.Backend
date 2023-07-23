using Backend.Modules.Groups.Contract;
using Backend.Modules.Users.Contract;
using FastEndpoints;

namespace Backend.Modules.Courses.Contract;

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
            AssignedGroups = r.AssignedGroups
                .Select(g => groupMapper.FromEntity(g)),
            Owners = r.Owners.Select(e => userMapper.FromEntity(e)),
            Semester = r.Semester
        };
    }
}