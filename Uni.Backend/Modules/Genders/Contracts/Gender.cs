using Uni.Backend.Data;


namespace Uni.Backend.Modules.Genders.Contracts;

public class Gender : BaseModel {
  public required string Name { get; set; }
}