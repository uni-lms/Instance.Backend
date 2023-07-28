using Uni.Backend.Modules.CourseContents.Abstractions;
using Uni.Backend.Modules.Static.Contracts;

namespace Uni.Backend.Modules.CourseContents.File.Contracts;

public class FileContent: BaseCourseContent
{
    public required StaticFile File { get; set; }
}