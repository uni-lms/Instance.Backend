using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using Uni.Backend.Data;
using Uni.Backend.Modules.CourseContents.Quiz.Contracts;


namespace Uni.Backend.Modules.CourseContents.Quiz.Endpoints;

public class StartQuizPassAttempt : Endpoint<StartQuizPassAttemptRequest, QuizPassAttempt> {
  private readonly AppDbContext _db;

  public StartQuizPassAttempt(AppDbContext db) {
    _db = db;
  }

  public override void Configure() {
    Version(1);
    Post("/materials/quiz/{id}/start-attempt");
    Options(x => x.WithTags("Course Materials"));
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

  public override async Task HandleAsync(StartQuizPassAttemptRequest req, CancellationToken ct) {
    var quiz = await _db.QuizContents.Where(e => e.Id == req.Quiz).FirstOrDefaultAsync(ct);

    if (quiz is null) {
      ThrowError(e => e.Quiz, "Quiz was not found", 404);
    }

    var user = await _db.Users.AsNoTracking().Where(e => e.Email == User.Identity!.Name).FirstAsync(ct);

    var attempt = new QuizPassAttempt {
      Quiz = quiz,
      AccruedPoints = Enumerable.Empty<AccruedPoint>().ToList(),
      User = user,
      StartedAt = DateTime.UtcNow,
    };

    await _db.QuizPassAttempts.AddAsync(attempt, ct);
    await _db.SaveChangesAsync(ct);

    await SendOkAsync(attempt, cancellation: ct);
  }
}