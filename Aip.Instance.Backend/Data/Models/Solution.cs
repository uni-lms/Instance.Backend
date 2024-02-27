using Aip.Instance.Backend.Data.Common;


namespace Aip.Instance.Backend.Data.Models;

public class Solution : BaseModel {
  public Assignment Assignment { get; set; }
  public User Author { get; set; }
  public ICollection<Comment> Comments { get; set; }
  public Uri? Link { get; set; }
  public StaticFile? File { get; set; }
}