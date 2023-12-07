using System.Text.Json.Serialization;

using Uni.Backend.Data;
using Uni.Backend.Modules.CourseBlocks.Contracts;
using Uni.Backend.Modules.Courses.Contracts;


namespace Uni.Backend.Modules.CourseContents.Abstractions;

public abstract class BaseCourseContent : BaseModel {
  [JsonIgnore]
  public required Course Course { get; init; }

  public required CourseBlock Block { get; set; }

  public bool IsVisibleToStudents { get; set; }
}