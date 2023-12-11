using Uni.Backend.Modules.Users.Contracts;


namespace Uni.Backend.Background.Mailings.Contracts;

public record CreateGroupMailingContract {
  public required UserCredentials Credentials { get; init; }
  public required string GroupName { get; init; }
}