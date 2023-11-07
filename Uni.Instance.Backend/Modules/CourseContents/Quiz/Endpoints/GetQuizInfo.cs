using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using Uni.Backend.Data;
using Uni.Backend.Modules.Common.Contracts;
using Uni.Backend.Modules.CourseContents.Quiz.Contracts;
using Uni.Instance.Backend.Modules.CourseContents.Quiz.Contracts;


namespace Uni.Instance.Backend.Modules.CourseContents.Quiz.Endpoints;

public class GetQuizInfo : Endpoint<SearchEntityRequest, QuizDetails> {
  private readonly AppDbContext _db;

  public GetQuizInfo(AppDbContext db) {
    _db = db;
  }

  public override void Configure() {
    Version(1);
    Get("/materials/quiz/{id}/details");
    Options(x => x.WithTags("Course Materials. Quizzes"));
    Description(b => b
      .ClearDefaultProduces()
      .Produces(200)
      .ProducesProblemFE(401)
      .ProducesProblemFE(403)
      .ProducesProblemFE(404)
      .ProducesProblemFE(500));
    Summary(x => {
      x.Summary = "Get details of quiz";
      x.Description = "<b>Allowed scopes:</b> Any authorized user";
      x.Responses[200] = "Quiz details fetched successfully";
      x.Responses[401] = "Not authorized";
      x.Responses[403] = "Access forbidden";
      x.Responses[404] = "Quiz was not found";
      x.Responses[500] = "Some other error occured";
    });
  }

  public override async Task HandleAsync(SearchEntityRequest req, CancellationToken ct) {
    var quizContent = await _db.QuizContents
      .Where(e => e.Id == req.Id)
      .Include(e => e.Questions)
      .FirstOrDefaultAsync(ct);

    if (quizContent is null) {
      ThrowError(e => e.Id, "Quiz was not found", 404);
    }

    var email = User.Identity?.Name;

    var user = await _db.Users.Where(e => e.Email == email).FirstOrDefaultAsync(ct);

    if (user is null) {
      ThrowError("Unauthorized", 401);
    }

    var attempts = _db.QuizPassAttempts
      .Where(e => e.User.Email == email && e.Quiz.Id == req.Id);

    var quizDetails = new QuizDetails {
      VisibleName = quizContent.Title,
      AmountOfQuestions = quizContent.Questions.Count,
      AmountOfAttempts = quizContent.AmountOfAllowedAttempts,
      Attempts = await attempts.Select(e => new AttemptInfo {
        Id = e.Id,
        StartedAt = e.StartedAt,
        FinishedAt = e.FinishedAt,
        AccruedPoints = e.AccruedPoints.Sum(k => k.AmountOfPoints),
      }).ToListAsync(ct),
      TimeLimit = quizContent.TimeLimit?.Minutes,
      RemainingAttempts = quizContent.AmountOfAllowedAttempts - await attempts.CountAsync(ct),
      HasStartedAttempt = await attempts.Where(e => e.FinishedAt == null).AnyAsync(ct),
      MaximumPoints = quizContent.Questions.Sum(e => e.MaximumPoints),
    };

    await SendAsync(quizDetails, cancellation: ct);
  }
}