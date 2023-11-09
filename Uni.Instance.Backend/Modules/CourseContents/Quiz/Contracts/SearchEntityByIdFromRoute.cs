using Microsoft.AspNetCore.Mvc;


namespace Uni.Instance.Backend.Modules.CourseContents.Quiz.Contracts; 

public class SearchEntityByIdFromRoute {
  [FromBody]
  public Guid Id { get; set; }
}