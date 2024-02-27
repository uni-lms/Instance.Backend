namespace Aip.Instance.Backend.Data.Models;

public class CommentClosure {
  public Comment Ancestor { get; set; }
  public Guid AncestorId { get; set; }
  public Comment Descendant { get; set; }
  public Guid DescendantId { get; set; }
  public int Depth { get; set; }
}