using FastEndpoints;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc;


namespace Uni.Backend.Modules.Courses.Contracts;

public class UpdateCourseRequest {
  [FromRoute]
  [BindFrom("id")]
  public Guid Id { get; [UsedImplicitly] set; }

  public required string Name { get; [UsedImplicitly] set; }
  public required string Abbreviation { get; [UsedImplicitly] set; }
  public int Semester { get; [UsedImplicitly] set; }
  public required List<Guid> AssignedGroups { get; [UsedImplicitly] set; }
  public required List<Guid> Owners { get; [UsedImplicitly] set; }
  public required List<Guid> Blocks { get; [UsedImplicitly] set; }
}