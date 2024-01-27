using Uni.Instance.Backend.Data.Common;


namespace Uni.Instance.Backend.Api.Groups.Data;

public class CreateGroupResponse : BaseModel {
  public required string Name { get; set; }
}