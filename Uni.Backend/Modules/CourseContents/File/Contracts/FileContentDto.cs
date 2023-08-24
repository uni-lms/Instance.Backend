using JetBrains.Annotations;

using Uni.Backend.Modules.Static.Contracts;


namespace Uni.Backend.Modules.CourseContents.File.Contracts;

public class FileContentDto {
  public bool IsVisibleToStudents { [UsedImplicitly] get; init; }
  public required StaticFileDto File { [UsedImplicitly] get; init; }
}