using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using Uni.Backend.Data;
using Uni.Backend.Modules.Common.Contracts;
using Uni.Backend.Modules.CourseContents.Quiz.Contracts;


namespace Uni.Backend.Modules.CourseContents.Quiz.Endpoints;

public class
  GetQuizPassAttemptDetails : Endpoint<SearchEntityRequest, QuizPassAttemptDetails,
    QuizPassAttemptDetailsMapper> {
  private readonly AppDbContext _db;

  public GetQuizPassAttemptDetails(AppDbContext db) {
    _db = db;
  }

  public override void Configure() {
    Version(1);
    Get("/quiz-attempt/{id}/details");
    Options(x => x.WithTags("Course Materials"));
    Description(b => b
      .ClearDefaultProduces()
      .Produces(200)
      .ProducesProblemFE(401)
      .ProducesProblemFE(403)
      .ProducesProblemFE(404)
      .ProducesProblemFE(500));
    Summary(x => {
      x.Summary = "Get details of quiz pass attempt";
      x.Description = "<b>Allowed scopes:</b> Any authorized user";
      x.Responses[200] = "Quiz pass attempt details fetched successfully";
      x.Responses[401] = "Not authorized";
      x.Responses[403] = "Access forbidden";
      x.Responses[404] = "Quiz was not found";
      x.Responses[500] = "Some other error occured";
    });
  }

  public override async Task HandleAsync(SearchEntityRequest req, CancellationToken ct) {
    var attempt = await _db.QuizPassAttempts
      .Where(e => e.Id == req.Id)
      .Include(e => e.AccruedPoints)
      .ThenInclude(e => e.Question)
      .FirstOrDefaultAsync(ct);

    if (attempt is null) {
      ThrowError(e => e.Id, "Attempt was not found", 404);
    }

    await SendAsync(Map.FromEntity(attempt), cancellation: ct);
  }
}