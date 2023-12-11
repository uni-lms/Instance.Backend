namespace Uni.Backend.Modules.AssignmentSolutions.Contracts; 

public class SolutionDto {
  public Guid Id { get; set; }
  public DateTime DateTime { get; set; }
  public int AmountOfFiles { get; set; }
  public required string Status { get; set; }
}
