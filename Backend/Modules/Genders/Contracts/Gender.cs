using Backend.Data;

namespace Backend.Modules.Genders.Contracts;

public class Gender: BaseModel
{
    public required string Name { get; set; }
}