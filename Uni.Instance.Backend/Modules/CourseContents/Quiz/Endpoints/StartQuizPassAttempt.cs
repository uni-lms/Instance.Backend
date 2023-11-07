using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using Uni.Backend.Data;
using Uni.Backend.Modules.Common.Contracts;
using Uni.Backend.Modules.CourseContents.Quiz.Contracts;


namespace Uni.Instance.Backend.Modules.CourseContents.Quiz.Endpoints;

public class StartQuizPassAttempt : Endpoint<SearchEntityRequest, QuizPassAttemptDto> {
  private readonly AppDbContext _db;

  public StartQuizPassAttempt(AppDbContext db) {
    _db = db;
  }

  public override void Configure() {
    Version(1);
    Post("/materials/quiz/{id}/start-attempt");
    Options(x => x.WithTags("Course Materials. Quizzes"));
    Description(b => b
      .ClearDefaultProduces()
      .Produces(200)
      .ProducesProblemFE(401)
      .ProducesProblemFE(403)
      .ProducesProblemFE(404)
      .ProducesProblemFE(500));
    Summary(x => {
      x.Summary = "Starts new quiz pass attempt";
      x.Description = "<b>Allowed scopes:</b> Any authorized user";
      x.Responses[200] = "Quiz pass attempt started successfully";
      x.Responses[401] = "Not authorized";
      x.Responses[403] = "Access forbidden";
      x.Responses[404] = "Quiz was not found";
      x.Responses[500] = "Some other error occured";
    });
  }

  public override async Task HandleAsync(SearchEntityRequest req, CancellationToken ct) {
    var quiz = await _db.QuizContents.Where(e => e.Id == req.Id).FirstOrDefaultAsync(ct);

    if (quiz is null) {
      ThrowError(e => e.Id, "Quiz was not found", 404);
    }

    var user = await _db.Users.AsNoTracking().Where(e => e.Email == User.Identity!.Name).FirstAsync(ct);

    var amountOfSavedAttempts = await _db.QuizPassAttempts
      .Where(e => e.User.Email == user.Email && e.Quiz.Id == quiz.Id)
      .CountAsync(ct);

    if (amountOfSavedAttempts >= quiz.AmountOfAllowedAttempts) {
      ThrowError("You can't start another attempt for the quiz, limit exceeded", 409);
    }

    var attempt = new QuizPassAttempt {
      Quiz = quiz,
      AccruedPoints = Enumerable.Empty<AccruedPoint>().ToList(),
      User = user,
      StartedAt = DateTime.UtcNow,
    };


    await _db.QuizPassAttempts.AddAsync(attempt, ct);
    await _db.SaveChangesAsync(ct);
    
    var response = new QuizPassAttemptDto {
      Id = attempt.Id,
    };

    await SendOkAsync(response, cancellation: ct);
  }
}