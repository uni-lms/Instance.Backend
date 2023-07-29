using Uni.Backend.Modules.Users.Contract;

namespace Uni.Backend.Background.Contracts;

public record CreateGroupMailingContract
{
    public required UserCredentials Credentials { get; set; }
}