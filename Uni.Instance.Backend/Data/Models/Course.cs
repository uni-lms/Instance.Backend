using System.ComponentModel.DataAnnotations;

using Uni.Instance.Backend.Data.Common;


namespace Uni.Instance.Backend.Data.Models;

public class Course : BaseModel {
  [MaxLength(50)]
  public required string Name { get; set; }

  [MaxLength(10)]
  public required string Abbreviation { get; set; }

  public required List<Group> AssignedGroups { get; set; }
  public int Semester { get; set; }
  public required List<User> Owners { get; set; }
}