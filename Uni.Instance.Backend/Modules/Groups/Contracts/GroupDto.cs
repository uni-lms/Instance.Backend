﻿using JetBrains.Annotations;


namespace Uni.Backend.Modules.Groups.Contracts;

public class GroupDto {
  public required Guid Id { [UsedImplicitly] get; set; }
  public required int AmountOfStudents { [UsedImplicitly] get; set; }
  public required string Name { [UsedImplicitly] get; set; }
  public int CurrentSemester { [UsedImplicitly] get; set; }
  public int MaxSemester { [UsedImplicitly] get; set; }
}