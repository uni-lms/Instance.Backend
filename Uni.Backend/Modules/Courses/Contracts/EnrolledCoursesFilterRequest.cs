using FastEndpoints;

namespace Uni.Backend.Modules.Courses.Contracts;

public class EnrolledCoursesFilterRequest
{
    [QueryParam]
    [BindFrom("filter")]
    public required string Filter { get; set; }
}