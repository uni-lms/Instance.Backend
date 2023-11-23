namespace Uni.Backend.Modules.AssignmentSolutions.Contracts; 

public class SolutionDto {
  public Guid Id { get; set; }
  public DateTime DateTime { get; set; }
  public int AmountOfFiles { get; set; }
  public string Status { get; set; }
}

// @Serializable(UUIDSerializer::class)
// val id: UUID,
// @Serializable(InstantSerializer::class)
// val dateTime: Instant,
// val amountOfFiles: Int,
// val status: String,