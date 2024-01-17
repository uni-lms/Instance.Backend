using Microsoft.AspNetCore.Mvc;


namespace Uni.Instance.Backend.Endpoints.Users.Data;

public class EditUserRequest {
  [FromRoute]
  public Guid Id { get; set; }

  public string? Email { get; set; }
  public string? FirstName { get; set; }
  public string? LastName { get; set; }
  public string? Patronymic { get; set; }
}