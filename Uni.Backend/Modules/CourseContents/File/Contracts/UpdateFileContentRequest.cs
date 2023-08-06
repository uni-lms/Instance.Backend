using FastEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace Uni.Backend.Modules.CourseContents.File.Contracts;

public class UpdateFileContentRequest
{
    [FromRoute]
    [BindFrom("id")]
    public Guid Id { get; set; }

    public required string VisibleName { get; set; }
    public Guid Block { get; set; }
}