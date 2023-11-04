namespace Uni.Instance.Backend.Modules.Courses.Contracts; 

public class CourseItemDto {
  public Guid Id { get; set; }
  public required string VisibleName { get; set; }
  public CourseItemType Type { get; set; }
}