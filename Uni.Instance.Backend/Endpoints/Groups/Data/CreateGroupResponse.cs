using Uni.Instance.Backend.Data.Common;


namespace Uni.Instance.Backend.Endpoints.Groups.Data;

public class CreateGroupResponse : BaseModel {
  public required string Name { get; set; }
}