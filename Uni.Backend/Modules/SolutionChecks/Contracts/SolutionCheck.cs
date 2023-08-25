using Uni.Backend.Data;
using Uni.Backend.Modules.AssignmentSolutions.Contracts;
using Uni.Backend.Modules.SolutionComments.Contracts;
using Uni.Backend.Modules.Users.Contracts;


namespace Uni.Backend.Modules.SolutionChecks.Contracts;

public class SolutionCheck: BaseModel {
  public required User CheckedBy { get; set; }
  public required AssignmentSolution Solution { get; set; }
  public required List<SolutionComment> Comments { get; set; }
  public int Points { get; set; }
}