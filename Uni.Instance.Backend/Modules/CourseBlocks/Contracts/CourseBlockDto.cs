using Uni.Instance.Backend.Modules.Courses.Contracts;


namespace Uni.Instance.Backend.Modules.CourseBlocks.Contracts; 

public class CourseBlockDto {
  public required string Title { get; set; }
  public required List<CourseItemDto> Items { get; set; }
}