using System.Net.Mime;
using Backend.Configuration;
using Backend.Data;
using Backend.Modules.Courses.Contract;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Backend.Modules.Courses.Endpoints;

public class GetEnrolledCourses : Endpoint<EnrolledCoursesFilterRequest, List<CourseDto>, CoursesMapper>
{
    private static readonly string[] AllowedFilters = new[] { "archived", "current", "upcoming" };
    private readonly AppDbContext _db;

    public GetEnrolledCourses(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Get("/courses/enrolled");
        Description(b => b
            .Produces<List<CourseDto>>(200, MediaTypeNames.Application.Json)
            .ProducesProblemFE<InternalErrorResponse>(500));
        Options(x => x.WithTags("Courses"));
        Summary(s =>
        {
            s.Summary = "Get all courses, in which current user is enrolled (with filter by semester)";
            s.Description = "Allowed roles: students";
            s.RequestParam(
                r => r.Filter,
                """Filtering rule for courses.<br>Allowed values: <code>archived</code>, <code>current</code>, <code>upcoming</code>""");
        });
        Version(1);
        Roles(UserRoles.Student);
    }

    public override async Task HandleAsync(EnrolledCoursesFilterRequest req, CancellationToken ct)
    {
        if (!AllowedFilters.Contains(req.Filter))
        {
            ThrowError(_ => "filter", $"Specified filter {req.Filter} is not allowed");
        }

        if (User.Identity is null)
        {
            ThrowError(_ => User, "Not authorized", 401);
        }

        var user = await _db.Users
            .Where(e => e.Email == User.Identity.Name)
            .FirstOrDefaultAsync(ct);

        if (user is null)
        {
            ThrowError(_ => User.Identity!.Name!, "User not found", 404);
        }

        var groupOfUser = await _db.Groups.Where(e => e.Students.Contains(user)).FirstOrDefaultAsync(ct);

        if (groupOfUser is null)
        {
            ThrowError(_ => "Group", "User doesn't exist in any of groups");
        }

        ;

        var filteredCourses = req.Filter switch
        {
            "archived" => _db.Courses.Where(e =>
                    e.AssignedGroups.Contains(groupOfUser) && e.Semester < groupOfUser.CurrentSemester)
                .Include(e => e.Owners)
                .Select(e => Map.FromEntity(e)),
            "current" => _db.Courses.Where(e =>
                    e.AssignedGroups.Contains(groupOfUser) && e.Semester == groupOfUser.CurrentSemester)
                .Include(e => e.Owners)
                .Select(e => Map.FromEntity(e)),
            "upcoming" => _db.Courses.Where(e =>
                    e.AssignedGroups.Contains(groupOfUser) && e.Semester > groupOfUser.CurrentSemester)
                .Include(e => e.Owners)
                .Select(e => Map.FromEntity(e)),
            _ => throw new ArgumentOutOfRangeException()
        };

        await SendAsync(await filteredCourses.ToListAsync(ct), 200, ct);
    }
}