﻿using JetBrains.Annotations;


namespace Uni.Backend.Modules.Users.Contracts;

public class UserDto {
  public required Guid Id { get; set; }
  public required string FirstName { [UsedImplicitly] get; set; }
  public required string LastName { [UsedImplicitly] get; set; }
  public string? Patronymic { [UsedImplicitly] get; set; }
  public string? RoleName { get; set; }
}