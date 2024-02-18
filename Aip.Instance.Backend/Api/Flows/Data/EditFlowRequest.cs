namespace Aip.Instance.Backend.Api.Flows.Data;

public class EditFlowRequest : IFlowRequest {
  public Guid Id { get; set; }
  public required string Name { get; set; }
}