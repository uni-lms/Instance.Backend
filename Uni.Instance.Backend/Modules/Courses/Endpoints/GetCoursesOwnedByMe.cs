using System.Net.Mime;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using Uni.Backend.Configuration;
using Uni.Backend.Data;
using Uni.Backend.Modules.Courses.Contracts;
using Uni.Instance.Backend.Modules.Courses.Contracts;


namespace Uni.Instance.Backend.Modules.Courses.Endpoints;

public class GetCoursesOwnedByMe : EndpointWithoutRequest<List<CourseDto>, CoursesMapper> {
  private readonly AppDbContext _db;

  public GetCoursesOwnedByMe(AppDbContext db) {
    _db = db;
  }

  public override void Configure() {
    Version(1);
    Get("/courses/owned");
    Roles(UserRoles.MinimumRequired(UserRoles.Tutor));
    Options(x => x.WithTags("Courses"));
    Description(b => b
      .Produces<List<CourseDto>>(200, MediaTypeNames.Application.Json)
      .ProducesProblemFE(401)
      .ProducesProblemFE(403)
      .ProducesProblemFE(500));
    Summary(x => {
      x.Summary = "Gets all courses owned by current user";
      x.Description = "<b>Allowed scopes:</b> Tutor, Administrator";
      x.Responses[200] = "List of courses fetched successfully";
      x.Responses[401] = "Not authorized";
      x.Responses[403] = "Access forbidden";
      x.Responses[500] = "Some other error occured";
    });
  }

  public override async Task HandleAsync(CancellationToken ct) {
    if (User.Identity is null) {
      ThrowError(_ => User, "Not authorized", 401);
    }

    var user = await _db.Users
      .AsNoTracking()
      .Where(e => e.Email == User.Identity!.Name!)
      .Include(e => e.OwnedCourses!)
      .ThenInclude(e => e.AssignedGroups)
      .FirstOrDefaultAsync(ct);

    if (user is null) {
      ThrowError(_ => User.Identity!.Name!, "User not found", 404);
    }

    var courses = user.OwnedCourses!
      .Select(e => Map.FromEntity(e))
      .ToList();

    await SendAsync(courses, cancellation: ct);
  }
}