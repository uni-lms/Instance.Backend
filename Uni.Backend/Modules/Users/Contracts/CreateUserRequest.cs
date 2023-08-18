using JetBrains.Annotations;


namespace Uni.Backend.Modules.Users.Contracts;

public class CreateUserRequest {
  public required string FirstName { get; [UsedImplicitly] set; }
  public required string LastName { get; [UsedImplicitly] set; }
  public required string Patronymic { get; [UsedImplicitly] set; }
  public DateOnly DateOfBirth { get; [UsedImplicitly] set; }
  public required string Email { get; [UsedImplicitly] set; }
  public required Guid Gender { get; [UsedImplicitly] set; }
}