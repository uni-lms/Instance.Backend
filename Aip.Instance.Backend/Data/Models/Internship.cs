using System.ComponentModel.DataAnnotations;

using Aip.Instance.Backend.Data.Common;


namespace Aip.Instance.Backend.Data.Models;

public class Internship : BaseModel {
  [MaxLength(50)]
  public required string Name { get; set; }

  public required List<Flow> AssignedFlows { get; set; }
  public ICollection<InternshipUserRole> InternshipUserRoles { get; set; }
}