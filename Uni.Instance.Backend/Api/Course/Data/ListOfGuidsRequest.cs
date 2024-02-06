namespace Uni.Instance.Backend.Api.Course.Data;

public class ListOfGuidsRequest {
  public Guid EntityId { get; set; }
  public required List<Guid> Ids { get; set; }
}