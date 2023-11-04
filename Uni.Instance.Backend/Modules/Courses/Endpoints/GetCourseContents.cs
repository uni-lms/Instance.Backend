using System.Net.Mime;
using System.Security.Claims;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using Uni.Backend.Configuration;
using Uni.Backend.Data;
using Uni.Backend.Modules.Common.Contracts;
using Uni.Backend.Modules.CourseContents.File.Contracts;
using Uni.Backend.Modules.CourseContents.Quiz.Contracts;
using Uni.Backend.Modules.CourseContents.Text.Contract;
using Uni.Backend.Modules.Static.Contracts;
using Uni.Instance.Backend.Modules.CourseBlocks.Contracts;
using Uni.Instance.Backend.Modules.Courses.Contracts;


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
    var isStudent = User.HasClaim(ClaimTypes.Role, UserRoles.Student);

    var course = await _db.Courses
      .AsNoTracking()
      .Where(e => e.Id == req.Id)
      .Include(e => e.Owners)
      .Include(e => e.Blocks)
      .FirstOrDefaultAsync(ct);

    if (course is null) {
      ThrowError(e => e.Id, "Course was not found", 404);
    }

    var blocks = new List<CourseBlockDto>();

    foreach (var block in course.Blocks) {
      blocks.Add(new CourseBlockDto() {
        Items = new List<CourseItemDto>(),
        Title = block.Name,
      });
    }

    var textContents = await _db.TextContents
      .Where(e => e.Course.Id == req.Id && (!isStudent || e.IsVisibleToStudents))
      .Include(e => e.Block)
      .Include(e => e.Content)
      .ToListAsync(ct);

    var fileContents = await _db.FileContents
      .Where(e => e.Course.Id == req.Id && (!isStudent || e.IsVisibleToStudents))
      .Include(e => e.Block)
      .Include(e => e.File)
      .ToListAsync(ct);

    var quizzes = await _db.QuizContents
      .Where(e => e.Course.Id == req.Id && (!isStudent || e.IsVisibleToStudents))
      .Include(quizContent => quizContent.CourseBlock)
      .ToListAsync(ct);

    foreach (var textContent in textContents) {
      var item = new CourseItemDto {
        Id = textContent.Id,
        Type = CourseItemType.Text,
        VisibleName = textContent.Content.VisibleName,
      };
      var block = blocks.First(e => e.Title == textContent.Block.Name);
      block.Items.Add(item);
    }
    
    foreach (var fileContent in fileContents) {
      var item = new CourseItemDto {
        Id = fileContent.Id,
        Type = CourseItemType.File,
        VisibleName = fileContent.File.VisibleName,
      };
      var block = blocks.First(e => e.Title == fileContent.Block.Name);
      block.Items.Add(item);
    }
    
    foreach (var quizContent in quizzes) {
      var item = new CourseItemDto {
        Id = quizContent.Id,
        Type = CourseItemType.Quiz,
        VisibleName = quizContent.Title,
      };
      var block = blocks.First(e => e.Title == quizContent.CourseBlock.Name);
      block.Items.Add(item);
    }

    var dto = new CourseContentsDto {
      Name = course.Name,
      Abbreviation = course.Abbreviation,
      Semester = course.Semester,
      Blocks = blocks,
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