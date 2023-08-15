using FastEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace Uni.Backend.Modules.CourseContents.Common.Contracts;

public class UpdateContentRequest
{
    [FromRoute]
    [BindFrom("id")]
    public Guid Id { get; set; }

    public required string VisibleName { get; set; }
    public Guid Block { get; set; }
}