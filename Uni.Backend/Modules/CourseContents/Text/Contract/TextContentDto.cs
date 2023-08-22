using JetBrains.Annotations;

using Uni.Backend.Modules.Static.Contracts;


namespace Uni.Backend.Modules.CourseContents.Text.Contract; 

public class TextContentDto {
  public bool IsVisibleToStudents { [UsedImplicitly] get; init; }
  public required StaticFileDto Content { [UsedImplicitly] get; init; }
}