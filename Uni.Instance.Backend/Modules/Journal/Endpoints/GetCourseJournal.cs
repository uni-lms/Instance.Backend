using System.Security.Claims;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using Uni.Backend.Configuration;
using Uni.Backend.Data;
using Uni.Backend.Modules.Common.Contracts;
using Uni.Instance.Backend.Modules.Courses.Contracts;
using Uni.Instance.Backend.Modules.Journal.Contracts;


namespace Uni.Instance.Backend.Modules.Journal.Endpoints;

public class GetCourseJournal : Endpoint<SearchEntityRequest, JournalDto> {
  private readonly AppDbContext _db;

  public GetCourseJournal(AppDbContext db) {
    _db = db;
  }

  public override void Configure() {
    Version(1);
    Get("/course/{id}/journal");
    Options(x => x.WithTags("Courses"));
    Description(b => b
      .ClearDefaultProduces()
      .Produces(200)
      .ProducesProblemFE(401)
      .ProducesProblemFE(403)
      .ProducesProblemFE(404)
      .ProducesProblemFE(500));
    Summary(x => {
      x.Summary = "Get journal of current user for specific course";
      x.Description = "<b>Allowed scopes:</b> Any authorized user";
      x.Responses[200] = "Journal fetched successfully";
      x.Responses[401] = "Not authorized";
      x.Responses[403] = "Access forbidden";
      x.Responses[404] = "Course was not found";
      x.Responses[500] = "Some other error occured";
    });
  }

  public override async Task HandleAsync(SearchEntityRequest req, CancellationToken ct) {
    var email = User.Identity?.Name;

    var user = await _db.Users.Where(e => e.Email == email).FirstOrDefaultAsync(ct);
    var isStudent = User.HasClaim(ClaimTypes.Role, UserRoles.Student);

    if (user is null) {
      ThrowError("Unauthorized", 401);
    }

    var course = await _db.Courses
      .Where(e => e.Id == req.Id)
      .FirstOrDefaultAsync(ct);

    if (course is null) {
      ThrowError(e => e.Id, "Course was not found", 404);
    }

    var quizzes = await _db.QuizContents
      .Where(e => e.Course.Id == req.Id && (!isStudent || e.IsVisibleToStudents))
      .Include(quizContent => quizContent.Questions)
      .ToListAsync(ct);

    var assignments = await _db.Assignments
      .Where(e => e.Course.Id == req.Id && (!isStudent || e.IsVisibleToStudents))
      .ToListAsync(ct);

    var journalItems = new List<JournalItem>();

    foreach (var quiz in quizzes) {
      var attempt = await _db.QuizPassAttempts
        .Where(e => e.Quiz.Id == quiz.Id && e.User.Id == user.Id)
        .OrderByDescending(e => e.StartedAt)
        .FirstOrDefaultAsync(ct);

      var status = "Не пройден";
      if (attempt is not null) {
        var accruedPoints = attempt.AccruedPoints.Sum(e => e.AmountOfPoints);
        var maximumPoints = quiz.Questions.Sum(e => e.MaximumPoints);
        status = $"{accruedPoints} / {maximumPoints}";
      }

      journalItems.Add(new JournalItem {
        Id = quiz.Id,
        Name = quiz.Title,
        Type = CourseItemType.Quiz,
        Status = status,
      });
    }

    foreach (var assignment in assignments) {
      var solutions = await _db.AssignmentSolutions
        .Include(e => e.Author)
        .Include(e => e.Team)
        .ThenInclude(e => e.Members)
        .Where(e => e.Assignment.Id == req.Id && ((e.Author != null && e.Author.Id == user.Id) ||
          (e.Team != null && e.Team.Members.Contains(user))))
        .Include(e => e.Checks)
        .ToListAsync(ct);


      var status = "Не отправлено";
      if (solutions.Count > 0) {
        var amountOfChecks = 0;
        foreach (var solution in solutions) {
          amountOfChecks += solution.Checks.Count;
        }

        if (amountOfChecks > 0) {
          var rating = solutions.Max(e => e.Checks.Max(sc => sc.Points));
          status = $"{rating} / {assignment.MaximumPoints}";
        }
        else {
          status = "Не проверено";
        }
        
      }
      journalItems.Add(new JournalItem {
        Id = assignment.Id,
        Name = assignment.Title,
        Type = CourseItemType.Task,
        Status = status,
      });
    }

    var dto = new JournalDto {
      CourseName = course.Name,
      Items = journalItems,
    };

    await SendAsync(dto, cancellation: ct);
  }
}