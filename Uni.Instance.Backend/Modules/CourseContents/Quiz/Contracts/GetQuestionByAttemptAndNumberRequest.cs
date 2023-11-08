using Microsoft.AspNetCore.Mvc;


namespace Uni.Instance.Backend.Modules.CourseContents.Quiz.Contracts; 

public class GetQuestionByAttemptAndNumberRequest {
  [FromRoute]
  public Guid AttemptId { get; set; }
  
  [FromRoute]
  public int Question { get; set; }
}