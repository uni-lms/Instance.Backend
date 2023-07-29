using Uni.Backend.Modules.Users.Contracts;

namespace Uni.Backend.Background.Contracts;

public record CreateGroupMailingContract
{
    public required UserCredentials Credentials { get; set; }
}