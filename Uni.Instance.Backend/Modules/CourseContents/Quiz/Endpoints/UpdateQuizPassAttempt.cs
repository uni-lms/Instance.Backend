using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using Uni.Backend.Data;
using Uni.Backend.Modules.CourseContents.Quiz.Contracts;
using Uni.Instance.Backend.Modules.CourseContents.Quiz.Contracts;


namespace Uni.Backend.Modules.CourseContents.Quiz.Endpoints;

public class UpdateQuizPassAttempt : Endpoint<UpdateQuizPassAttemptRequest, QuizPassAttempt> {
  private readonly AppDbContext _db;

  public UpdateQuizPassAttempt(AppDbContext db) {
    _db = db;
  }

  public override void Configure() {
    Version(1);
    Patch("/quiz-attempt/{id}");
    Options(x => x.WithTags("Course Materials. Quizzes"));
    Description(b => b
      .ClearDefaultProduces()
      .Produces(200)
      .ProducesProblemFE(401)
      .ProducesProblemFE(403)
      .ProducesProblemFE(404)
      .ProducesProblemFE(500));
    Summary(x => {
      x.Summary = "Update quiz pass attempt (change answer to question)";
      x.Description = "<b>Allowed scopes:</b> Any authorized user";
      x.Responses[200] = "Quiz pass attempt updated successfully";
      x.Responses[401] = "Not authorized";
      x.Responses[403] = "Access forbidden";
      x.Responses[404] = "Some related entity was not found";
      x.Responses[500] = "Some other error occured";
    });
  }

  public override async Task HandleAsync(UpdateQuizPassAttemptRequest req, CancellationToken ct) {
    var attempt = await _db.QuizPassAttempts
      .Where(e => e.Id == req.PassAttempt)
      .Include(e => e.Quiz)
      .Include(e => e.AccruedPoints)
      .ThenInclude(e => e.Question)
      .FirstOrDefaultAsync(ct);


    if (attempt is null) {
      ThrowError(e => e.PassAttempt, "Pass attempt was not found", 404);
    }

    var question = await _db.MultipleChoiceQuestions
      .Where(e => e.Id == req.Question)
      .Include(e => e.Choices)
      .FirstOrDefaultAsync(ct);

    if (question is null) {
      ThrowError(e => e.Question, "Question was not found", 404);
    }

    var searchForAnswer = attempt.AccruedPoints.FirstOrDefault(e => e.Question == question);

    if (req.SelectedChoices.Count > 1 && !question.IsMultipleChoicesAllowed) {
      ThrowError(e => e.SelectedChoices, "This question allow only one answer");
    }

    var amountOfCorrectChoices = question.Choices.Count(e => e.IsCorrect);
    var amountOfCorrectSelectedChoices = await _db.QuestionChoices
      .Where(e => req.SelectedChoices.Contains(e.Id) && e.IsCorrect)
      .CountAsync(ct);

    var pointsSumOfCorrectSelected = await _db.QuestionChoices
      .Where(e => req.SelectedChoices.Contains(e.Id) && e.IsCorrect)
      .SumAsync(e => e.AmountOfPoints, ct);

    var points = 0;

    if (amountOfCorrectChoices == amountOfCorrectSelectedChoices) {
      points = pointsSumOfCorrectSelected;
    }
    else if (amountOfCorrectChoices != amountOfCorrectSelectedChoices &&
      question.IsGivingPointsForIncompleteAnswersEnabled) {
      points = pointsSumOfCorrectSelected * (amountOfCorrectChoices / amountOfCorrectSelectedChoices);
    }

    var accruedPoint = new AccruedPoint {
      Question = question,
      AmountOfPoints = points,
    };

    if (searchForAnswer is null) {
      attempt.AccruedPoints.Add(accruedPoint);
    }
    else {
      searchForAnswer.AmountOfPoints = points;
    }

    await _db.SaveChangesAsync(ct);

    await SendOkAsync(attempt, cancellation: ct);
  }
}