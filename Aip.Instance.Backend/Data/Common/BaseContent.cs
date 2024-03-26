using Aip.Instance.Backend.Data.Models;


namespace Aip.Instance.Backend.Data.Common;

public abstract class BaseContent : BaseModel {
  public required Internship Internship { get; set; }
  public required Section Section { get; set; }

  public bool IsVisibleToInterns { get; set; }
}