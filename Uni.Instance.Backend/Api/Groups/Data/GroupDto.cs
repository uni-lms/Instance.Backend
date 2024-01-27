namespace Uni.Instance.Backend.Api.Groups.Data;

public class GroupDto {
  public Guid Id { get; set; }
  public required string Name { get; set; }
  public int EnteringYear { get; set; }
  public int GraduationYear { get; set; }
}