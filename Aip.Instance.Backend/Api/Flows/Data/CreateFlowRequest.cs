namespace Aip.Instance.Backend.Api.Flows.Data;

public class CreateFlowRequest : IFlowRequest {
  public required string Name { get; set; }
}