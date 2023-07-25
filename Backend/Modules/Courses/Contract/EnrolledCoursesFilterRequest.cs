using FastEndpoints;

namespace Backend.Modules.Courses.Contract;

public class EnrolledCoursesFilterRequest
{
    [QueryParam]
    [BindFrom("filter")]
    public required string Filter { get; set; }
}