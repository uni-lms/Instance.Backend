using System.ComponentModel.DataAnnotations;

using Aip.Instance.Backend.Data.Common;


namespace Aip.Instance.Backend.Data.Models;

public sealed class Role : BaseModel {
  [MaxLength(12)]
  public required string Name { get; set; }
}