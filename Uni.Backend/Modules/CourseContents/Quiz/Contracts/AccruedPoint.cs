﻿using Uni.Backend.Data;


namespace Uni.Backend.Modules.CourseContents.Quiz.Contracts;

public class AccruedPoint : BaseModel {
  public required MultipleChoiceQuestion Question { get; set; }
  public int AmountOfPoints { get; set; }
}