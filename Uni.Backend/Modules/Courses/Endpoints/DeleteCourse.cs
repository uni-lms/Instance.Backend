using System.Net.Mime;
using FastEndpoints;
using Uni.Backend.Configuration;
using Uni.Backend.Data;
using Uni.Backend.Modules.Common.Contract;

namespace Uni.Backend.Modules.Courses.Endpoints;

public class DeleteCourse: Endpoint<SearchEntityRequest>
{
    private readonly AppDbContext _db;

    public DeleteCourse(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Version(1);
        Delete("/courses/{Id}");
        Roles(UserRoles.Administrator);
        Options(x => x.WithTags("Courses"));
        Description(b => b
            .ClearDefaultProduces()
            .Produces<EmptyResponse>(204, MediaTypeNames.Application.Json)
            .ProducesProblemFE(401)
            .ProducesProblemFE(403)
            .ProducesProblemFE(404)
            .ProducesProblemFE(500));
        Summary(x =>
        {
            x.Summary = "Permanently deletes course";
            x.Description = """
                               <b>Allowed scopes:</b> Administrator
                            """;
            x.Responses[204] = "Course deleted";
            x.Responses[401] = "Not authorized";
            x.Responses[403] = "Access forbidden";
            x.Responses[404] = "Group was not found";
            x.Responses[500] = "Some other error occured";
        });
    }

    public override async Task HandleAsync(SearchEntityRequest req, CancellationToken ct)
    {
        
        var course = await _db.Courses.FindAsync(new object?[] { req.Id }, cancellationToken: ct);

        if (course is null)
        {
            ThrowError("Course was not found", 404);
        }
        
        _db.Courses.Remove(course);
        await _db.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
    }
}