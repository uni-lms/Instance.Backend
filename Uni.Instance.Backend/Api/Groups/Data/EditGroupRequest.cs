namespace Uni.Instance.Backend.Api.Groups.Data;

public class EditGroupRequest : IGroupRequest {
  public Guid Id { get; set; }
  public required string Name { get; set; }
  public int EnteringYear { get; set; }
  public int YearsOfStudy { get; set; }
}