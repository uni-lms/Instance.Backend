using System.Security.Claims;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Uni.Backend.Configuration;
using Uni.Backend.Data;
using Uni.Backend.Modules.Common.Contracts;

namespace Uni.Backend.Modules.CourseContents.Text.Endpoints;

public class DeleteTextContents: Endpoint<SearchEntityRequest>
{
    private readonly AppDbContext _db;

    public DeleteTextContents(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Version(1);
        Roles(UserRoles.MinimumRequired(UserRoles.Tutor));
        Delete("/materials/text/{id}");
        Options(x => x.WithTags("Course Materials"));
        Description(b => b
            .ClearDefaultProduces()
            .Produces(204)
            .ProducesProblemFE(401)
            .ProducesProblemFE(403)
            .ProducesProblemFE(404)
            .ProducesProblemFE(409)
            .ProducesProblemFE(500));
        Summary(x =>
        {
            x.Summary = "Permanently deletes text content from course";
            x.Description = """
                               <b>Allowed scopes:</b> Any Administrator, Tutor who ownes the course
                            """;
            x.Responses[204] = "Content deleted successfully";
            x.Responses[401] = "Not authorized";
            x.Responses[403] = "Access forbidden";
            x.Responses[404] = "Content was not found";
            x.Responses[500] = "Some other error occured";
        });
    }
    
    public override async Task HandleAsync(SearchEntityRequest req, CancellationToken ct)
    {
        var textContent = await _db.TextContents
            .Where(e => e.Id == req.Id)
            .Include(e => e.Course)
            .ThenInclude(e => e.Owners)
            .FirstOrDefaultAsync(ct);

        if (textContent is null)
        {
            ThrowError(e => e.Id, "Text content was not found", 404);
        }
        
        var user = await _db.Users.AsNoTracking().Where(e => e.Email == User.Identity!.Name).FirstAsync(ct);

        if (User.HasClaim(ClaimTypes.Role, UserRoles.Tutor) && !textContent.Course.Owners.Contains(user))
        {
            ThrowError(_ => User, "Access forbidden", 403);
        }
        
        System.IO.File.Delete(textContent.Content.FilePath);

        _db.StaticFiles.Remove(textContent.Content);
        _db.TextContents.Remove(textContent);

        await _db.SaveChangesAsync(ct);
        await SendNoContentAsync(cancellation: ct);

    }
}