namespace Aip.Instance.Backend.Api.Internships.Data;

public class InviteInternRequest {
  public Guid Id { get; set; }
  public required string FirstName { get; set; }
  public required string LastName { get; set; }
  public string? Patronymic { get; set; }
  public required string Email { get; set; }
}