using Backend.Data;
using Backend.Modules.CourseBlocks.Contracts;
using Backend.Modules.Courses.Contract;
using Backend.Modules.Static.Contracts;

namespace Backend.Modules.CourseContents.Abstractions;

public abstract class BaseCourseContent: BaseModel
{
    public required Course Course { get; set; }
    public required CourseBlock Block { get; set; }
    public bool IsVisibleToStudents { get; set; }
    public required StaticFile Icon { get; set; }
}