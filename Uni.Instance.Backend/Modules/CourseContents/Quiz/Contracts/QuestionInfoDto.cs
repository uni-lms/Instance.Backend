using Uni.Backend.Data;


namespace Uni.Instance.Backend.Modules.CourseContents.Quiz.Contracts; 

public class QuestionInfoDto: BaseModel {
  public required string QuizTitle { get; set; }
  public required string QuestionTitle { get; set; }
  public int SequenceNumber { get; set; }
  public int AmountOfQuestions { get; set; }
  public bool IsMultipleChoicesAllowed { get; set; }
  public required List<QuestionChoiceDto> Choices { get; set; }
}