using FastEndpoints;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc;


namespace Uni.Backend.Modules.CourseContents.Common.Contracts;

public class UpdateContentRequest {
  [FromRoute]
  [BindFrom("id")]
  public Guid Id { get; [UsedImplicitly] set; }

  public required string VisibleName { get; [UsedImplicitly] set; }
  public Guid Block { get; [UsedImplicitly] set; }
}