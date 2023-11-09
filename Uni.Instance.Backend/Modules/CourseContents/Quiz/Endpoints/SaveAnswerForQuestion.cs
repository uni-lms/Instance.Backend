using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using Uni.Backend.Data;
using Uni.Backend.Modules.CourseContents.Quiz.Contracts;
using Uni.Instance.Backend.Modules.CourseContents.Quiz.Contracts;


namespace Uni.Instance.Backend.Modules.CourseContents.Quiz.Endpoints;

public class SaveAnswerForQuestion : Endpoint<SaveAnswerForQuestionRequest, AccruedPointDto> {
  private readonly AppDbContext _db;

  public SaveAnswerForQuestion(AppDbContext db) {
    _db = db;
  }

  public override void Configure() {
    Version(1);
    Post("/quiz-attempt/{attemptId}/save-answer");
    Options(x => x.WithTags("Course Materials. Quizzes"));
    Description(b => b
      .ClearDefaultProduces()
      .Produces(200)
      .ProducesProblemFE(401)
      .ProducesProblemFE(403)
      .ProducesProblemFE(404)
      .ProducesProblemFE(500));
    Summary(x => {
      x.Summary = "Save user's answer for a question";
      x.Description = "<b>Allowed scopes:</b> Any authorized user";
      x.Responses[200] = "Quiz pass attempt details fetched successfully";
      x.Responses[401] = "Not authorized";
      x.Responses[403] = "Access forbidden";
      x.Responses[404] = "Quiz was not found";
      x.Responses[500] = "Some other error occured";
    });
  }

  public override async Task HandleAsync(SaveAnswerForQuestionRequest req, CancellationToken ct) {
    var attempt = await _db.QuizPassAttempts
      .Where(e => e.Id == req.AttemptId)
      .Include(e => e.AccruedPoints)
      .FirstOrDefaultAsync(ct);


    if (attempt is null) {
      ThrowError(e => e.AttemptId, "Attempt was not found", 404);
    }

    var question = await _db.MultipleChoiceQuestions
      .Where(e => e.Id == req.QuestionId)
      .Include(e => e.Choices)
      .FirstOrDefaultAsync(ct);

    if (question is null) {
      ThrowError(e => e.QuestionId, "Question was not found", 404);
    }

    var points = 0;

    var selectedChoices = new List<QuestionChoice>();

    foreach (var choice in req.Choices) {
      var data = await _db.QuestionChoices
        .Where(e => e.Id == choice.Id)
        .FirstOrDefaultAsync(ct);

      if (data is null) {
        ThrowError(e => e.Choices, "Choice was not found");
      }

      if (data.IsCorrect) {
        points += data.AmountOfPoints;
      }

      if (selectedChoices.All(e => e.Id != data.Id)){
        selectedChoices.Add(data);
      }
    }

    var maximumPoints = question.Choices.Where(e => e.IsCorrect).Sum(e => e.AmountOfPoints);
    if (question is { IsMultipleChoicesAllowed: true, IsGivingPointsForIncompleteAnswersEnabled: false } &&
      points < maximumPoints) {
      points = 0;
    }

    var accruedPoints = new AccruedPoint {
      Question = question,
      AmountOfPoints = points,
      SelectedChoices = selectedChoices,
    };

    var previousAnswer = attempt.AccruedPoints.FirstOrDefault(e => e.Question == question);
    
    if (previousAnswer is not null) {
      previousAnswer.AmountOfPoints = points;
      previousAnswer.SelectedChoices = selectedChoices;
    }
    else {
      attempt.AccruedPoints.Add(accruedPoints);
    }

    await _db.SaveChangesAsync(ct);

    var accruedPointsDto = new AccruedPointDto {
      QuestionTitle = question.Text,
      AmountOfPoints = points,
      MaximumPoints = maximumPoints,
    };

    await SendAsync(accruedPointsDto, cancellation: ct);
  }
}