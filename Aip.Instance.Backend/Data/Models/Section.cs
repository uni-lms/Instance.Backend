using System.ComponentModel.DataAnnotations;

using Aip.Instance.Backend.Data.Common;


namespace Aip.Instance.Backend.Data.Models;

public class Section : BaseModel {
  [MaxLength(40)]
  public required string Name { get; set; }
}