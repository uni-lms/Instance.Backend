using JetBrains.Annotations;

using Uni.Backend.Modules.Users.Contracts;


namespace Uni.Backend.Modules.Groups.Contracts;

public class CreateGroupDto {
  public required GroupDto Group { [UsedImplicitly] get; set; }
  public required List<UserCredentials> UsersData { [UsedImplicitly] get; set; }
}