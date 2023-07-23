using System.Net.Mime;
using Backend.Data;
using Backend.Modules.Courses.Contract;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Backend.Modules.Courses.Endpoints;

public class GetAllCourses : EndpointWithoutRequest<List<CourseDto>, CoursesMapper>
{
    private readonly AppDbContext _db;

    public GetAllCourses(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Get("/courses");
        Description(b => b
            .Produces<List<CourseDto>>(200, MediaTypeNames.Application.Json)
            .ProducesProblemFE<InternalErrorResponse>(500));
        Options(x => x.WithTags("Courses"));
        Version(1);
        Roles("Administrator");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _db.Courses
            .Include(e => e.AssignedGroups)
            .Include(e => e.Owner)
            .ThenInclude(e => e.Role)
            .Select(e => Map.FromEntity(e))
            .ToListAsync(ct);
        await SendAsync(result, cancellation: ct);
    }
}