using FastEndpoints;

using Microsoft.AspNetCore.Mvc;


namespace Uni.Backend.Modules.CourseContents.Quiz.Contracts;

public class UpdateQuizRequest: CreateQuizRequest {
  [FromRoute]
  [BindFrom("id")]
  public Guid Id { get; set; }
}