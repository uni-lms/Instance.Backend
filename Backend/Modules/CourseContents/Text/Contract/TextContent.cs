using Backend.Modules.CourseContents.Abstractions;
using Backend.Modules.Static.Contracts;

namespace Backend.Modules.CourseContents.Text.Contract;

public class TextContent: BaseCourseContent
{
    public required StaticFile Content { get; set; }
}