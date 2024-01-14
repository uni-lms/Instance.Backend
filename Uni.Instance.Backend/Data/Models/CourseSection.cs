using System.ComponentModel.DataAnnotations;

using Uni.Instance.Backend.Data.Common;


namespace Uni.Instance.Backend.Data.Models;

public class CourseSection : BaseModel {
  [MaxLength(40)]
  public required string Name { get; set; }
}