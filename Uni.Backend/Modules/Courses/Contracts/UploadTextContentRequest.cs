using FastEndpoints;

namespace Uni.Backend.Modules.Courses.Contracts;

public class UploadTextContentRequest
{
    [BindFrom("courseId")]
    public Guid CourseId { get; set; }
    public Guid BlockId { get; set; }
    public required IFormFile Content { get; set; }
    public bool IsVisibleToStudents { get; set; }
    public required string VisibleName { get; set; }
}