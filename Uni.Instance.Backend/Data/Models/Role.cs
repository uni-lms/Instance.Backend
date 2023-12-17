﻿using System.ComponentModel.DataAnnotations;

using Uni.Instance.Backend.Data.Common;


namespace Uni.Instance.Backend.Data.Models;

public sealed class Role: BaseModel {
  [MaxLength(7)]
  public required string Name { get; set; }
}