using Uni.Backend.Data;
using Uni.Backend.Modules.Courses.Contracts;


namespace Uni.Backend.Modules.CourseBlocks.Contracts;

public class CourseBlock : BaseModel {
  public required string Name { get; set; }
  public required List<Course> Courses { get; set; }
}