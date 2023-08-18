using System.Net.Mime;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using Uni.Backend.Data;
using Uni.Backend.Modules.Common.Contracts;
using Uni.Backend.Modules.Courses.Contracts;


namespace Uni.Backend.Modules.Courses.Endpoints;

public class GetCourseContents : Endpoint<SearchEntityRequest, CourseContentsDto> {
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

    var textContents = _db.TextContents
      .Where(e => e.Id == req.Id)
      .Include(e => e.Block)
      .GroupBy(e => e.Block.Name)
      .ToDictionaryAsync(e => e.Key, e => e.ToList(), ct);

    var fileContents = _db.FileContents
      .Where(e => e.Id == req.Id)
      .Include(e => e.Block)
      .GroupBy(e => e.Block.Name)
      .ToDictionaryAsync(e => e.Key, e => e.ToList(), ct);

    var dto = new CourseContentsDto {
      Name = course.Name,
      Semester = course.Semester,
      Owners = owners,
      TextContents = await textContents,
      FileContents = await fileContents
    };

    await SendAsync(dto, cancellation: ct);
  }
}