using FastEndpoints;
using JetBrains.Annotations;

namespace Uni.Backend.Modules.Courses.Contracts;

public class EnrolledCoursesFilterRequest
{
    [QueryParam]
    [BindFrom("filter")]
    public required string Filter { get; [UsedImplicitly] set; }
}