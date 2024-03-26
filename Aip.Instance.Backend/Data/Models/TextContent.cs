using System.ComponentModel.DataAnnotations;

using Aip.Instance.Backend.Data.Common;


namespace Aip.Instance.Backend.Data.Models;

public class TextContent : BaseContent {
  [MaxLength(300)]
  public required string Text { get; set; }
}