using FastEndpoints;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc;


namespace Uni.Backend.Modules.Groups.Contracts;

public class EditGroupRequest {
  public Guid Id { get; [UsedImplicitly] set; }

  public required string Name { get; [UsedImplicitly] set; }
  public int CurrentSemester { get; [UsedImplicitly] set; }
  public int MaxSemester { get; [UsedImplicitly] set; }
}