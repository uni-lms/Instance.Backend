using System.Net.Mime;
using System.Security.Claims;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using Uni.Backend.Configuration;
using Uni.Backend.Data;
using Uni.Backend.Modules.Common.Contracts;
using Uni.Backend.Modules.CourseContents.File.Contracts;
using Uni.Backend.Modules.CourseContents.Quiz.Contracts;


namespace Uni.Backend.Modules.CourseContents.Quiz.Endpoints;

public class ToggleQuizVisibility : Endpoint<SearchEntityRequest, QuizDto, QuizMapper> {
  private readonly AppDbContext _db;

  public ToggleQuizVisibility(AppDbContext db) {
    _db = db;
  }

  public override void Configure() {
    Version(1);
    Roles(UserRoles.MinimumRequired(UserRoles.Tutor));
    Patch("/materials/quiz/{id}/toggle-visibility");
    Options(x => x.WithTags("Course Materials"));
    Description(b => b
      .ClearDefaultProduces()
      .Produces<FileContent>(200, MediaTypeNames.Application.Json)
      .ProducesProblemFE(401)
      .ProducesProblemFE(403)
      .ProducesProblemFE(404)
      .ProducesProblemFE(500));
    Summary(x => {
      x.Summary = "Toggles visibility of the quiz";
      x.Description = "<b>Allowed scopes:</b> Any Administrator, Tutor who ownes course to which the material belongs";
      x.Responses[200] = "Quiz visibility toggled successfully";
      x.Responses[401] = "Not authorized";
      x.Responses[403] = "Access forbidden";
      x.Responses[404] = "Quiz was not found";
      x.Responses[500] = "Some other error occured";
    });
  }

  public override async Task HandleAsync(SearchEntityRequest req, CancellationToken ct) {
    var quiz = await _db.QuizContents
      .Where(e => e.Id == req.Id)
      .Include(e => e.Course)
      .ThenInclude(e => e.Owners)
      .FirstOrDefaultAsync(ct);

    if (quiz is null) {
      ThrowError(e => e.Id, "Quiz was not found", 404);
    }

    var user = await _db.Users.AsNoTracking().Where(e => e.Email == User.Identity!.Name).FirstAsync(ct);

    if (User.HasClaim(ClaimTypes.Role, UserRoles.Tutor) && !quiz.Course.Owners.Contains(user)) {
      ThrowError(_ => User, "Access forbidden", 403);
    }

    quiz.IsVisibleToStudents = !quiz.IsVisibleToStudents;

    await _db.SaveChangesAsync(ct);
    await SendAsync(Map.FromEntity(quiz), cancellation: ct);
  }
}