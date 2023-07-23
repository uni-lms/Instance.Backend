using System.Net.Mime;
using Backend.Data;
using Backend.Modules.Courses.Contract;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Backend.Modules.Courses.Endpoints;

public class GetAllCourses : EndpointWithoutRequest<List<Course>>
{
    private readonly AppDbContext _db;

    public GetAllCourses(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Get("/api/courses");
        AllowAnonymous();
        Description(b => b
            .Produces<List<Course>>(200, MediaTypeNames.Application.Json)
            .ProducesProblemFE<InternalErrorResponse>(500));
        Options(x => x.WithTags("Courses"));
        Version(1);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _db.Courses.ToListAsync(ct);
        await SendAsync(result, cancellation: ct);
    }
}