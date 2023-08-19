using FastEndpoints;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc;


namespace Uni.Backend.Modules.CourseContents.Common.Contracts;

public class UploadContentRequest {
  [FromRoute]
  [BindFrom("courseId")]
  public Guid CourseId { get; [UsedImplicitly] set; }

  public Guid BlockId { get; [UsedImplicitly] set; }
  public required IFormFile Content { get; [UsedImplicitly] set; }
  public bool IsVisibleToStudents { get; [UsedImplicitly] set; }
  public required string VisibleName { get; [UsedImplicitly] set; }
}