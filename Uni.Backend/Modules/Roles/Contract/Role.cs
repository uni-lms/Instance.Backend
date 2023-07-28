using Uni.Backend.Data;

namespace Uni.Backend.Modules.Roles.Contract;

public class Role: BaseModel
{
    public required string Name { get; set; }
}