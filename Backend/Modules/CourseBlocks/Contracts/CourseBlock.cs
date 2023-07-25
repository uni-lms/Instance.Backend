using Backend.Data;

namespace Backend.Modules.CourseBlocks.Contracts;

public class CourseBlock: BaseModel
{
    public required string Name { get; set; }
}