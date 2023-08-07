using Uni.Backend.Modules.CourseContents.File.Contracts;
using Uni.Backend.Modules.CourseContents.Text.Contract;

namespace Uni.Backend.Modules.Courses.Contracts;

public class CourseContentsDto
{
    public required string Name { get; set; }
    public int Semester { get; set; }
    public required List<string> Owners { get; set; }
    public required Dictionary<string, List<TextContent>> TextContents { get; set; }
    public required Dictionary<string, List<FileContent>> FileContents { get; set; }
    
}