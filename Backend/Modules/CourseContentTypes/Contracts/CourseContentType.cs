using Backend.Data;

namespace Backend.Modules.CourseContentTypes.Contracts;

public class CourseContentType: BaseModel
{
    public required string Name { get; set; }
}