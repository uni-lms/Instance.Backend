﻿using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using Uni.Backend.Data;
using Uni.Backend.Modules.Common.Contracts;
using Uni.Backend.Modules.CourseContents.Quiz.Contracts;


namespace Uni.Backend.Modules.CourseContents.Quiz.Endpoints;

public class FinishQuizPassAttempt : Endpoint<SearchEntityRequest, QuizPassAttempt> {
  private readonly AppDbContext _db;

  public FinishQuizPassAttempt(AppDbContext db) {
    _db = db;
  }

  public override void Configure() {
    Version(1);
    Patch("/quiz-attempt/{id}/finish");
    Options(x => x.WithTags("Course Materials"));
    Description(b => b
      .ClearDefaultProduces()
      .Produces(200)
      .ProducesProblemFE(401)
      .ProducesProblemFE(403)
      .ProducesProblemFE(404)
      .ProducesProblemFE(500));
    Summary(x => {
      x.Summary = "Finishes quiz pass attempt";
      x.Description = "<b>Allowed scopes:</b> Any authorized user";
      x.Responses[200] = "Quiz pass attempt finished successfully";
      x.Responses[401] = "Not authorized";
      x.Responses[403] = "Access forbidden";
      x.Responses[404] = "Quiz was not found";
      x.Responses[500] = "Some other error occured";
    });
  }

  public override async Task HandleAsync(SearchEntityRequest req, CancellationToken ct) {
    var attempt = await _db.QuizPassAttempts
      .Where(e => e.Id == req.Id)
      .FirstOrDefaultAsync(ct);


    if (attempt is null) {
      ThrowError(e => e.Id, "Pass attempt was not found", 404);
    }
    
    attempt.FinishedAt = DateTime.UtcNow;

    await _db.SaveChangesAsync(ct);
    await SendOkAsync(attempt, cancellation: ct);
  }
}