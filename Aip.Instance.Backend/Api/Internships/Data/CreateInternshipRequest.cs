namespace Aip.Instance.Backend.Api.Internships.Data;

public class CreateInternshipRequest {
  public required string Name { get; set; }
  public required List<Guid> AssignedFlows { get; set; }
  public required List<Guid> Owners { get; set; }
}