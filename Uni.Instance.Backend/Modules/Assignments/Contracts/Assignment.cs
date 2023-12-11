using Uni.Backend.Modules.CourseContents.Abstractions;


namespace Uni.Backend.Modules.Assignments.Contracts;

public class Assignment: BaseCourseContent {
  public required string Title { get; set; }
  public string? Description { get; set; }
  public DateTime AvailableUntil { get; set; }
  public int MaximumPoints { get; set; }
}