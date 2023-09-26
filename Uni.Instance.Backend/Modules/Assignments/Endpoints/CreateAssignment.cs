using System.Net.Mime;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using Uni.Backend.Data;
using Uni.Backend.Modules.Assignments.Contracts;


namespace Uni.Backend.Modules.Assignments.Endpoints;

public class CreateAssignment : Endpoint<CreateAssignmentRequestBody, Assignment> {
  private readonly AppDbContext _db;

  public CreateAssignment(AppDbContext db) {
    _db = db;
  }

  public override void Configure() {
    Version(1);
    Post("/assignments");
    Options(x => x.WithTags("Assignments"));
    Description(b => b
      .Produces<Assignment>(201, MediaTypeNames.Application.Json)
      .ProducesProblemFE(401)
      .ProducesProblemFE(404)
      .ProducesProblemFE(500));
    Summary(x => {
      x.Summary = "Creates new assignment in course";
      x.Description = "<b>Allowed scopes:</b> Any Administrator, Tutor who ownes the course";
      x.Responses[201] = "Assignment was created successfully";
      x.Responses[401] = "Unauthorized";
      x.Responses[403] = "Forbidden";
      x.Responses[404] = "Some related entity was not found";
      x.Responses[500] = "Some other error occured";
    });
  }

  public override async Task HandleAsync(CreateAssignmentRequestBody req, CancellationToken ct) {
    var course = await _db.Courses.Where(e => e.Id == req.Course).FirstOrDefaultAsync(ct);

    if (course is null) {
      ThrowError(e => e.Course, "Course was not found", 404);
    }

    var block = await _db.CourseBlocks.Where(e => e.Id == req.Block).FirstOrDefaultAsync(ct);

    if (block is null) {
      ThrowError(e => e.Block, "Course block was not found", 404);
    }

    var assignment = new Assignment {
      Course = course,
      Block = block,
      Title = req.Title,
      Description = req.Description,
      AvailableUntil = req.AvailableUntil,
      IsVisibleToStudents = req.IsVisibleToStudents,
      MaximumPoints = req.MaximumPoints,
    };

    await _db.Assignments.AddAsync(assignment, ct);
    await _db.SaveChangesAsync(ct);

    await SendCreatedAtAsync("/assignments", null, assignment, cancellation: ct);
  }
}