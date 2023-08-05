using FastEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace Uni.Backend.Modules.Groups.Contracts;

public class EditGroupRequest
{
    [FromRoute]
    [BindFrom("id")]
    public Guid Id { get; set; }

    public required string Name { get; set; }
    public int CurrentSemester { get; set; }
    public int MaxSemester { get; set; }
}