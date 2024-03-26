using Aip.Instance.Backend.Data.Common;


namespace Aip.Instance.Backend.Data.Models;

public class Comment : BaseModel {
  public string Text { get; set; }
  public User Author { get; set; }
}