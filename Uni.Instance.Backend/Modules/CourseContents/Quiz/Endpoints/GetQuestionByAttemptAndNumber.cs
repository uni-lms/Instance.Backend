using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using Uni.Backend.Data;
using Uni.Backend.Modules.CourseContents.Quiz.Contracts;
using Uni.Instance.Backend.Modules.CourseContents.Quiz.Contracts;


namespace Uni.Instance.Backend.Modules.CourseContents.Quiz.Endpoints;

public class GetQuestionByAttemptAndNumber : Endpoint<GetQuestionByAttemptAndNumberRequest, MultipleChoiceQuestion> {
  private readonly AppDbContext _db;

  public GetQuestionByAttemptAndNumber(AppDbContext db) {
    _db = db;
  }

  public override void Configure() {
    Version(1);
    Get("/quiz-attempt/{AttemptId}/question/{Question}");
    Options(x => x.WithTags("Course Materials. Quizzes"));
    Description(b => b
      .ClearDefaultProduces()
      .Produces(200)
      .ProducesProblemFE(401)
      .ProducesProblemFE(403)
      .ProducesProblemFE(404)
      .ProducesProblemFE(500));
    Summary(x => {
      x.Summary = "Get details of question by attempt id and number of question";
      x.Description = "<b>Allowed scopes:</b> Any authorized user";
      x.Responses[200] = "Question details fetched successfully";
      x.Responses[401] = "Not authorized";
      x.Responses[403] = "Access forbidden";
      x.Responses[404] = "Quiz was not found";
      x.Responses[500] = "Some other error occured";
    });
  }

  public override async Task HandleAsync(GetQuestionByAttemptAndNumberRequest req, CancellationToken ct) {
    var quizPassAttempt = await _db.QuizPassAttempts
      .Where(e => e.Id == req.AttemptId)
      .Include(e => e.Quiz)
      .ThenInclude(e => e.Questions)
      .ThenInclude(e => e.Choices)
      .FirstOrDefaultAsync(ct);

    if (quizPassAttempt is null) {
      ThrowError(e => e.AttemptId, "Attempt was not found", 404);
    }

    var question = quizPassAttempt.Quiz.Questions.FirstOrDefault(e => e.SequenceNumber == req.Question);

    if (question is null) {
      ThrowError(e => e.Question, "Question was not found", 404);
    }

    await SendAsync(question, cancellation: ct);
  }
}