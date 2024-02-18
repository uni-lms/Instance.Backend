using Aip.Instance.Backend.Data.Common;


namespace Aip.Instance.Backend.Data.Models;

public class Flow : BaseModel {
  public required string Name { get; set; }
  public required List<User> Students { get; set; }
  public required List<Internship> Internships { get; set; }
}