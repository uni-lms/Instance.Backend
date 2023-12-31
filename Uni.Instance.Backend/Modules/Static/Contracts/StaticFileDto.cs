﻿using JetBrains.Annotations;


namespace Uni.Backend.Modules.Static.Contracts;

public class StaticFileDto {
  public required string Id { [UsedImplicitly] get; set; }
  public required string VisibleName { [UsedImplicitly] get; set; }
}