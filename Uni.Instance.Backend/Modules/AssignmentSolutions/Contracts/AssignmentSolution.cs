using Uni.Backend.Data;
using Uni.Backend.Modules.Assignments.Contracts;
using Uni.Backend.Modules.SolutionChecks.Contracts;
using Uni.Backend.Modules.Static.Contracts;
using Uni.Backend.Modules.Teams.Contracts;
using Uni.Backend.Modules.Users.Contracts;


namespace Uni.Backend.Modules.AssignmentSolutions.Contracts; 

public class AssignmentSolution: BaseModel {
  public required Assignment Assignment { get; set; }
  public User? Author { get; set; }
  public Team? Team { get; set; }
  public required List<StaticFile> Files { get; set; }
  public DateTime UploadedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public required List<SolutionCheck> Checks { get; set; }
}