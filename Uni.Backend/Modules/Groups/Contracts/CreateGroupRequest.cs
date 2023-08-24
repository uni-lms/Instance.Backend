using JetBrains.Annotations;

using Uni.Backend.Modules.Users.Contracts;


namespace Uni.Backend.Modules.Groups.Contracts;

public class CreateGroupRequest {
  public required string Name { get; [UsedImplicitly] set; }
  public int CurrentSemester { get; [UsedImplicitly] set; }
  public int MaxSemester { get; [UsedImplicitly] set; }
  public required List<CreateUserRequest> Users { get; [UsedImplicitly] set; }
}