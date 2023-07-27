using Backend.Configuration;
using Backend.Data;
using Backend.Modules.Common.Contract;
using FastEndpoints;

namespace Backend.Modules.Courses.Endpoints;

public class DeleteCourse: Endpoint<SearchEntityRequest>
{
    private readonly AppDbContext _db;

    public DeleteCourse(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Delete("/course");
        Options(x => x.WithTags("Courses"));
        Roles(UserRoles.Administrator);
        Version(1);
    }

    public override async Task HandleAsync(SearchEntityRequest req, CancellationToken ct)
    {
        
        var course = await _db.Courses.FindAsync(new object?[] { req.Id }, cancellationToken: ct);

        if (course is null)
        {
            ThrowError("File was not found", 404);
        }
        
        _db.Courses.Remove(course);
        await _db.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
    }
}