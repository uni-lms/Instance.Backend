using Uni.Backend.Data;
using Uni.Backend.Modules.CourseBlocks.Contracts;
using Uni.Backend.Modules.Courses.Contract;
using Uni.Backend.Modules.Static.Contracts;

namespace Uni.Backend.Modules.CourseContents.Abstractions;

public abstract class BaseCourseContent: BaseModel
{
    public required Course Course { get; set; }
    public required CourseBlock Block { get; set; }
    public bool IsVisibleToStudents { get; set; }
    public required StaticFile Icon { get; set; }
}