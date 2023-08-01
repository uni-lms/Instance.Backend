using FastEndpoints;
using Riok.Mapperly.Abstractions;
using Uni.Backend.Modules.Groups.Contracts;
using Uni.Backend.Modules.Users.Contracts;

namespace Uni.Backend.Modules.Courses.Contracts;

[Mapper]
public partial class CoursesMapper : ResponseMapper<CourseDto, Course>
{
    public partial CourseDto FromEntity(Course r);
}