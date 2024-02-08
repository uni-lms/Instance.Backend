using Uni.Instance.Backend.Data.Common;


namespace Uni.Instance.Backend.Data.Models;

public class CourseContentFile : BaseCourseContent {
  public required string Title { get; set; }
  public required StaticFile File { get; set; }
}