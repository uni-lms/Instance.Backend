using Uni.Backend.Data;

namespace Uni.Backend.Modules.CourseBlocks.Contracts;

public class CourseBlock: BaseModel
{
    public required string Name { get; set; }
}