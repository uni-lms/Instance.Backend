using Uni.Backend.Data;
using Uni.Backend.Modules.CourseBlocks.Contracts;
using Uni.Backend.Modules.Courses.Contracts;


namespace Uni.Backend.Modules.Assignments.Contracts;

public class Assignment: BaseModel {
  public required Course Course { get; set; }
  public required CourseBlock Block { get; set; }
  public required string Title { get; set; }
  public string? Description { get; set; }
  public DateTime AvailableUntil { get; set; }
  public bool IsVisibleToStudents { get; set; }
  public int MaximumPoints { get; set; }
}