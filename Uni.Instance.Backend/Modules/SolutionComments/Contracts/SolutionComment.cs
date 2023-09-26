using Uni.Backend.Data;
using Uni.Backend.Modules.Users.Contracts;


namespace Uni.Backend.Modules.SolutionComments.Contracts;

public class SolutionComment: BaseModel {
  public required User Author { get; set; }
  public required string Text { get; set; }
  public DateTime PostedAt { get; set; }
  public bool WasEdited { get; set; }
}