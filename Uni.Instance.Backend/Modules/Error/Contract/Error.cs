namespace Uni.Instance.Backend.Modules.Error.Contract; 

public class Error {
  public required string Reason { get; set; }
  public required int Code { get; set; }
}