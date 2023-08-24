using System.Net.Mime;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using Uni.Backend.Data;
using Uni.Backend.Modules.Common.Contracts;
using Uni.Backend.Modules.CourseContents.File.Contracts;
using Uni.Backend.Modules.CourseContents.Quiz.Contracts;
using Uni.Backend.Modules.CourseContents.Text.Contract;
using Uni.Backend.Modules.Courses.Contracts;
using Uni.Backend.Modules.Static.Contracts;


namespace Uni.Backend.Modules.Courses.Endpoints;

public class GetCourseContents : Endpoint<SearchEntityRequest, CourseContentsDto, QuizMapper> {
  private readonly AppDbContext _db;

  public GetCourseContents(AppDbContext db) {
    _db = db;
  }

  public override void Configure() {
    Version(1);
    Get("/courses/{id}/contents");
    Options(x => x.WithTags("Courses"));
    Description(b => b
      .ClearDefaultProduces()
      .Produces<CourseContentsDto>(200, MediaTypeNames.Application.Json)
      .ProducesProblemFE(401)
      .ProducesProblemFE(404)
      .ProducesProblemFE(500));
    Summary(x => {
      x.Summary = "Gets content of the course";
      x.Description = "<b>Allowed scopes:</b> Any authorized user";
      x.Responses[200] = "Course contents fetched successfully";
      x.Responses[401] = "Not authorized";
      x.Responses[404] = "Course was not found";
      x.Responses[500] = "Some other error occured";
    });
  }

  public override async Task HandleAsync(SearchEntityRequest req, CancellationToken ct) {
    var course = await _db.Courses
      .AsNoTracking()
      .Where(e => e.Id == req.Id)
      .Include(e => e.Owners)
      .FirstOrDefaultAsync(ct);

    if (course is null) {
      ThrowError(e => e.Id, "Course was not found", 404);
    }

    var owners = course.Owners.Select(e => {
      var patronymicInitials = e.Patronymic is not null ? $" {e.Patronymic[0]}." : "";
      return $"{e.LastName} {e.FirstName[0]}.{patronymicInitials}";
    }).ToList();

    var textContents = await _db.TextContents
      .Where(e => e.Course.Id == req.Id)
      .Include(e => e.Block)
      .Include(e => e.Content)
      .GroupBy(e => e.Block!.Name)
      .ToDictionaryAsync(e => e.Key, e => e.Select(TextContentToDto).ToList(), ct);

    var fileContents = await _db.FileContents
      .Where(e => e.Course.Id == req.Id)
      .Include(e => e.Block)
      .Include(e => e.File)
      .GroupBy(e => e.Block!.Name)
      .ToDictionaryAsync(e => e.Key, e => e.Select(FileContentToDto).ToList(), ct);

    var quizzes = await _db.QuizContents
      .Where(e => e.Course.Id == req.Id)
      .GroupBy(e => e.CourseBlock.Name)
      .ToDictionaryAsync(
        e => e.Key,
        e => e.Select(q => Map.FromEntity(q)).ToList(),
        cancellationToken: ct
      );

    var dto = new CourseContentsDto {
      Name = course.Name,
      Semester = course.Semester,
      Owners = owners,
      TextContents = textContents,
      FileContents = fileContents,
      Quizzes = quizzes,
    };

    await SendAsync(dto, cancellation: ct);
  }

  private FileContentDto FileContentToDto(FileContent e) {
    return new FileContentDto {
      IsVisibleToStudents = e.IsVisibleToStudents,
      File = new StaticFileDto {
        Id = e.File.Id,
        VisibleName = e.File.VisibleName,
      },
    };
  }

  private TextContentDto TextContentToDto(TextContent e) {
    return new TextContentDto {
      IsVisibleToStudents = e.IsVisibleToStudents,
      Content = new StaticFileDto {
        Id = e.Content.Id,
        VisibleName = e.Content.VisibleName,
      },
    };
  }
}