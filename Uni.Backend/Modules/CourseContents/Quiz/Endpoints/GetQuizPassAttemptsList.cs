using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using Uni.Backend.Data;
using Uni.Backend.Modules.Common.Contracts;
using Uni.Backend.Modules.CourseContents.Quiz.Contracts;


namespace Uni.Backend.Modules.CourseContents.Quiz.Endpoints;

public class GetQuizPassAttemptsList : Endpoint<SearchEntityRequest, List<QuizPassAttemptDto>, QuizPassAttemptMapper> {
  private readonly AppDbContext _db;

  public GetQuizPassAttemptsList(AppDbContext db) {
    _db = db;
  }

  public override void Configure() {
    Version(1);
    Get("/materials/quiz/{id}/attempts");
    Options(x => x.WithTags("Course Materials"));
    Description(b => b
      .ClearDefaultProduces()
      .Produces<List<QuizPassAttemptDto>>()
      .ProducesProblemFE(401)
      .ProducesProblemFE(403)
      .ProducesProblemFE(404)
      .ProducesProblemFE(500));
    Summary(x => {
      x.Summary = "Gets list of quiz pass attempts of current user";
      x.Description = "<b>Allowed scopes:</b> Any authorized user";
      x.Responses[200] = "List of quiz pass attempts fetched successfully";
      x.Responses[401] = "Not authorized";
      x.Responses[403] = "Access forbidden";
      x.Responses[404] = "Quiz was not found";
      x.Responses[500] = "Some other error occured";
    });
  }

  public override async Task HandleAsync(SearchEntityRequest req, CancellationToken ct) {
    if (User.Identity is null) {
      ThrowError(_ => User, "Not authorized", 401);
    }

    var user = await _db.Users
      .AsNoTracking()
      .Where(e => e.Email == User.Identity!.Name!)
      .FirstOrDefaultAsync(ct);

    if (user is null) {
      ThrowError(_ => User.Identity!.Name!, "User not found", 404);
    }

    var hasQuiz = await _db.QuizContents.AnyAsync(e => e.Id == req.Id, ct);

    if (!hasQuiz) {
      ThrowError(e => e.Id, "Quiz was not found", 404);
    }

    var attempts = await _db.QuizPassAttempts
      .Where(e => e.Quiz.Id == req.Id && e.User.Email == user.Email)
      .Include(e => e.AccruedPoints)
      .ToListAsync(ct);

    var mapped = attempts.Select(e => Map.FromEntity(e)).ToList();

    await SendAsync(mapped, cancellation: ct);
  }
}