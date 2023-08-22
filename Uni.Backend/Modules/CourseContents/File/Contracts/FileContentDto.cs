using Uni.Backend.Modules.Static.Contracts;


namespace Uni.Backend.Modules.CourseContents.File.Contracts; 

public class FileContentDto {
  public bool IsVisibleToStudents { get; init; }
  public required StaticFileDto File { get; init; }
}