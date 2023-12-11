using Uni.Backend.Modules.CourseContents.Abstractions;
using Uni.Backend.Modules.Static.Contracts;


namespace Uni.Backend.Modules.CourseContents.Text.Contract;

public class TextContent : BaseCourseContent {
  public required StaticFile Content { get; set; }
}