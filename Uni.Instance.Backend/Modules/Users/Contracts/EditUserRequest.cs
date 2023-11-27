using FastEndpoints;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc;


namespace Uni.Backend.Modules.Users.Contracts;

public class EditUserRequest {
  public Guid Id { get; [UsedImplicitly] set; }

  public required string FirstName { get; [UsedImplicitly] set; }
  public required string LastName { get; [UsedImplicitly] set; }
  public string? Patronymic { get; [UsedImplicitly] set; }
}