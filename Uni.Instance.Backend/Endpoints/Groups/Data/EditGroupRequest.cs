namespace Uni.Instance.Backend.Endpoints.Groups.Data;

public class EditGroupRequest {
  public Guid Id { get; set; }
  public required string Name { get; set; }
  public int EnteringYear { get; set; }
  public int YearsOfStudy { get; set; }
}