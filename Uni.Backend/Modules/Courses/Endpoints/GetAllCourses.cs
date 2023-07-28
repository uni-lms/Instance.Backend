using System.Net.Mime;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Uni.Backend.Configuration;
using Uni.Backend.Data;
using Uni.Backend.Modules.Courses.Contract;

namespace Uni.Backend.Modules.Courses.Endpoints;

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
        Roles(UserRoles.Administrator);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _db.Courses
            .Include(e => e.AssignedGroups)
            .Include(e => e.Owners)
            .ThenInclude(e => e.Role)
            .Select(e => Map.FromEntity(e))
            .ToListAsync(ct);
        await SendAsync(result, cancellation: ct);
    }
}