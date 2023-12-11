using System.Net.Mime;
using System.Security.Claims;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using Uni.Backend.Configuration;
using Uni.Backend.Data;
using Uni.Backend.Modules.CourseContents.Quiz.Contracts;
using Uni.Instance.Backend.Modules.CourseContents.Quiz.Contracts;


namespace Uni.Backend.Modules.CourseContents.Quiz.Endpoints;

public class CreateQuiz : Endpoint<CreateQuizRequest, QuizDto, QuizMapper> {
  private readonly AppDbContext _db;

  public CreateQuiz(AppDbContext db) {
    _db = db;
  }

  public override void Configure() {
    Version(1);
    Roles(UserRoles.MinimumRequired(UserRoles.Tutor));
    Post("/courses/{courseId}/quiz");
    Options(x => x.WithTags("Course Materials. Quizzes"));
    Description(b => b
      .ClearDefaultProduces()
      .Produces<QuizDto>(201, MediaTypeNames.Application.Json)
      .ProducesProblemFE(401)
      .ProducesProblemFE(403)
      .ProducesProblemFE(404)
      .ProducesProblemFE(409)
      .ProducesProblemFE(500));
    Summary(x => {
      x.Summary = "Creates quiz in the course";
      x.Description = "<b>Allowed scopes:</b> Any Administrator, Tutor who ownes the course";
      x.Responses[201] = "Quiz created successfully";
      x.Responses[401] = "Not authorized";
      x.Responses[403] = "Access forbidden";
      x.Responses[404] = "Some related entity was not found";
      x.Responses[409] = "This block was not enabled in the course";
      x.Responses[500] = "Some other error occured";
    });
  }

  public override async Task HandleAsync(CreateQuizRequest req, CancellationToken ct) {
    var course = await _db.Courses
      .Where(e => e.Id == req.CourseId)
      .Include(e => e.Blocks)
      .FirstOrDefaultAsync(ct);

    if (course is null) {
      ThrowError("Course was not found", 404);
    }

    var user = await _db.Users.AsNoTracking().Where(e => e.Email == User.Identity!.Name).FirstAsync(ct);

    if (User.HasClaim(ClaimTypes.Role, UserRoles.Tutor) && !course.Owners.Contains(user)) {
      ThrowError(_ => User, "Access forbidden", 403);
    }

    var block = await _db.CourseBlocks
      .Where(e => e.Id == req.BlockId)
      .FirstOrDefaultAsync(ct);

    if (block is null) {
      ThrowError("Course block was not found", 404);
    }

    if (course.Blocks.All(e => e.Id != block.Id)) {
      ThrowError("This block wasn't enabled in the course", 409);
    }

    var questions = new List<MultipleChoiceQuestion>();

    foreach (var (question, ind) in req.Questions.Select((q, i) => (q, i))) {
      var choices = question.Choices.Select(choice => new QuestionChoice {
        Text = choice.Text,
        IsCorrect = choice.IsCorrect,
        AmountOfPoints = choice.AmountOfPoints,
        AccruedPoints = Enumerable.Empty<AccruedPoint>().ToList(),
      }).ToList();

      var questionToCreate = new MultipleChoiceQuestion {
        Text = question.Text,
        Choices = choices,
        IsMultipleChoicesAllowed = question.IsMultipleChoicesAllowed,
        IsGivingPointsForIncompleteAnswersEnabled = question.IsGivingPointsForIncompleteAnswersEnabled,
        MaximumPoints = question.MaximumPoints,
        SequenceNumber = ind + 1,
      };

      questions.Add(questionToCreate);
    }

    var quiz = new QuizContent {
      Title = req.Title,
      Description = req.Description,
      TimeLimit = req.TimeLimit,
      IsQuestionsShuffled = req.IsQuestionsShuffled,
      AvailableUntil = req.AvailableUntil,
      Questions = questions,
      Course = course,
      CourseBlock = block,
      AmountOfAllowedAttempts = req.AmountOfAllowedAttempts,
    };

    await _db.QuizContents.AddAsync(quiz, ct);
    await _db.SaveChangesAsync(ct);

    await SendCreatedAtAsync("/quizzes", null, Map.FromEntity(quiz), cancellation: ct);
  }
}