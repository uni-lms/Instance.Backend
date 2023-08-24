using FastEndpoints;

using Riok.Mapperly.Abstractions;


namespace Uni.Backend.Modules.Courses.Contracts;

[Mapper]
public partial class CoursesMapper : ResponseMapper<CourseDto, Course> {
  public partial CourseDto FromEntity(Course r);
}