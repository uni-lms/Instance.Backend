using System.ComponentModel.DataAnnotations;

using Uni.Instance.Backend.Data.Common;


namespace Uni.Instance.Backend.Data.Models;

public sealed class User : BaseModel {
  [MaxLength(50)]
  public required string Email { get; set; }

  [MaxLength(30)]
  public required string FirstName { get; set; }

  [MaxLength(30)]
  public required string LastName { get; set; }

  [MaxLength(30)]
  public string? Patronymic { get; set; }

  public required byte[] PasswordHash { get; set; }
  public required byte[] PasswordSalt { get; set; }
  public required Role Role { get; set; }

  public override string ToString() {
    var patronymic = Patronymic is not null ? $" {Patronymic[0]}." : "";
    return $"{LastName} {FirstName[0]}.{patronymic}";
  }
}