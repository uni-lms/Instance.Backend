using System.Net.Mime;
using System.Security.Claims;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using Uni.Backend.Configuration;
using Uni.Backend.Data;
using Uni.Backend.Modules.CourseContents.Quiz.Contracts;


namespace Uni.Backend.Modules.CourseContents.Quiz.Endpoints;

public class CreateQuiz : Endpoint<CreateQuizRequest, QuizDto, QuizMapper> {
  private readonly AppDbContext _db;

  public CreateQuiz(AppDbContext db) {
    _db = db;
  }

  public override void Configure() {
    Version(1);
    Roles(UserRoles.MinimumRequired(UserRoles.Tutor));
    Post("/quiz");
    Options(x => x.WithTags("Quizzes"));
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

    var quiz = new QuizContent {
      Title = req.Title,
      Description = req.Description,
      TimeLimit = req.TimeLimit,
      IsQuestionsShuffled = req.IsQuestionsShuffled,
      AvailableUntil = req.AvailableUntil,
      Questions = new List<MultipleChoiceQuestion>(),
      Course = course,
      CourseBlock = block,
    };
    
    await _db.QuizContents.AddAsync(quiz, ct);
    await _db.SaveChangesAsync(ct);

    await SendCreatedAtAsync("/quizzes", null, Map.FromEntity(quiz), cancellation: ct);
  }
}