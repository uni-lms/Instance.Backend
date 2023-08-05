using FastEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace Uni.Backend.Modules.Courses.Contracts;

public class UpdateCourseRequest
{
    [FromRoute]
    [BindFrom("id")]
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Abbreviation { get; set; }
    public int Semester { get; set; }
    public required List<Guid> AssignedGroups { get; set; }
    public required List<Guid> Owners { get; set; }
    public required List<Guid> Blocks { get; set; }
}