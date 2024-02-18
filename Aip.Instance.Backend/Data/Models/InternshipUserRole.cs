namespace Aip.Instance.Backend.Data.Models;

public class InternshipUserRole {
  public Guid InternshipId { get; set; }
  public required Internship Internship { get; set; }

  public Guid UserId { get; set; }
  public required User User { get; set; }
  public Guid RoleId { get; set; }
  public required Role Role { get; set; }
}