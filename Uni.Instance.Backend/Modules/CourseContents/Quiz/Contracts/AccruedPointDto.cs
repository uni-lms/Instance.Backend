using JetBrains.Annotations;


namespace Uni.Backend.Modules.CourseContents.Quiz.Contracts; 

public class AccruedPointDto {
  public required string QuestionTitle { [UsedImplicitly] get; set; }
  public int AmountOfPoints { [UsedImplicitly] get; set; }
  public int MaximumPoints { [UsedImplicitly] get; set; }
}