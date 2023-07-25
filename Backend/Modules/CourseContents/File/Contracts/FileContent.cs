using Backend.Modules.CourseContents.Abstractions;
using Backend.Modules.Static.Contracts;

namespace Backend.Modules.CourseContents.File.Contracts;

public class FileContent: BaseCourseContent
{
    public required StaticFile File { get; set; }
}