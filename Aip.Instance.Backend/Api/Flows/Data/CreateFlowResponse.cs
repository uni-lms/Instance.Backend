using Aip.Instance.Backend.Data.Common;


namespace Aip.Instance.Backend.Api.Flows.Data;

public class CreateFlowResponse : BaseModel {
  public required string Name { get; set; }
}