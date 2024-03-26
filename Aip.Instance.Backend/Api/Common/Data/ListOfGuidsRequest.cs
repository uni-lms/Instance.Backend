namespace Aip.Instance.Backend.Api.Common.Data;

public class ListOfGuidsRequest {
  public Guid EntityId { get; set; }
  public required List<Guid> Ids { get; set; }
}