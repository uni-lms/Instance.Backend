namespace Uni.Instance.Backend.Api.Groups.Data;

public interface IGroupRequest {
  public string Name { get; set; }
  public int EnteringYear { get; set; }
  public int YearsOfStudy { get; set; }
}