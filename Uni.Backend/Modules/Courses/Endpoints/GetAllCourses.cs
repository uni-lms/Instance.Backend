using System.Net.Mime;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Uni.Backend.Configuration;
using Uni.Backend.Data;
using Uni.Backend.Modules.Courses.Contracts;

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
        Version(1);
        Get("/courses");
        Roles(UserRoles.Administrator);
        Options(x => x.WithTags("Courses"));
        Description(b => b
            .Produces<List<CourseDto>>(200, MediaTypeNames.Application.Json)
            .ProducesProblemFE(401)
            .ProducesProblemFE(403)
            .ProducesProblemFE(500));
        Summary(x =>
        {
            x.Summary = "Gets list of all courses";
            x.Description = "<b>Allowed scopes:</b> Administrator";
            x.Responses[200] = "List of courses fetched successfully";
            x.Responses[401] = "Not authorized";
            x.Responses[403] = "Access forbidden";
            x.Responses[500] = "Some other error occured";
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var courses = await _db.Courses
            .AsNoTracking()
            .Include(e => e.AssignedGroups)
            .Include(e => e.Owners)
            .ThenInclude(e => e.Role)
            .ToListAsync(ct);

        var result = courses.Select(e => Map.FromEntity(e)).ToList();

        await SendAsync(result, cancellation: ct);
    }
}