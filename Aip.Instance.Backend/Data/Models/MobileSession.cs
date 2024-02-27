using Aip.Instance.Backend.Data.Common;


namespace Aip.Instance.Backend.Data.Models;

public class MobileSession : BaseModel {
  public required User User { get; set; }
  public required string FcmToken { get; set; }
}