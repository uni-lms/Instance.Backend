namespace Uni.Backend.Modules.CourseContents.Quiz.Contracts; 

public class AccruedPointDto {
  public required string QuestionTitle { get; set; }
  public int AmountOfPoints { get; set; }
  public int MaximumPoints { get; set; }
}