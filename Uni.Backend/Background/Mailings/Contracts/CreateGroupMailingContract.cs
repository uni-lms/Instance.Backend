using Uni.Backend.Modules.Users.Contracts;

namespace Uni.Backend.Background.Mailings.Contracts;

public record CreateGroupMailingContract
{
    public required UserCredentials Credentials { get; set; }
    public required string GroupName { get; set; }
}