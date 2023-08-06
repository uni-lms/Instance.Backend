using System.Net.Mime;
using System.Security.Claims;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Uni.Backend.Configuration;
using Uni.Backend.Data;
using Uni.Backend.Modules.CourseContents.Common.Contracts;
using Uni.Backend.Modules.CourseContents.File.Contracts;

namespace Uni.Backend.Modules.CourseContents.File.Endpoints;

public class UpdateFileContent : Endpoint<UpdateContentRequest, FileContent>
{
    private readonly AppDbContext _db;

    public UpdateFileContent(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Version(1);
        Roles(UserRoles.MinimumRequired(UserRoles.Tutor));
        Put("/materials/file/{id}");
        Options(x => x.WithTags("Course Materials"));
        Description(b => b
            .ClearDefaultProduces()
            .Produces<FileContent>(200, MediaTypeNames.Application.Json)
            .ProducesProblemFE(401)
            .ProducesProblemFE(403)
            .ProducesProblemFE(404)
            .ProducesProblemFE(409)
            .ProducesProblemFE(500));
        Summary(x =>
        {
            x.Summary = "Updates file content";
            x.Description = """
                            <b>Allowed scopes:</b> Any Administrator, Tutor who ownes the course
                            """;
            x.Responses[201] = "Content updated successfully";
            x.Responses[401] = "Not authorized";
            x.Responses[403] = "Access forbidden";
            x.Responses[404] = "Content or course block was not found";
            x.Responses[404] = "Specified block is not enabled in the course";
            x.Responses[500] = "Some other error occured";
        });
    }

    public override async Task HandleAsync(UpdateContentRequest req, CancellationToken ct)
    {
        var fileContent = await _db.FileContents
            .Where(e => e.Id == req.Id)
            .Include(e => e.File)
            .Include(e => e.Course)
            .ThenInclude(e => e.Blocks)
            .Include(e => e.Course)
            .ThenInclude(e => e.Owners)
            .FirstOrDefaultAsync(ct);

        if (fileContent is null)
        {
            ThrowError(e => e.Id, "File content was not found", 404);
        }

        var user = await _db.Users.AsNoTracking().Where(e => e.Email == User.Identity!.Name).FirstAsync(ct);

        if (User.HasClaim(ClaimTypes.Role, UserRoles.Tutor) && !fileContent.Course.Owners.Contains(user))
        {
            ThrowError(_ => User, "Access forbidden", 403);
        }

        var block = await _db.CourseBlocks
            .AsNoTracking()
            .Where(e => e.Id == req.Block)
            .FirstOrDefaultAsync(ct);

        if (block is null)
        {
            ThrowError(e => e.Block, "Course block was not found", 404);
        }

        if (!fileContent.Course.Blocks.Contains(block))
        {
            ThrowError(e => e.Block, "This block is not enabled in the course", 409);
        }

        fileContent.File.VisibleName = req.VisibleName;
        fileContent.Block = block;

        await _db.SaveChangesAsync(ct);
        await SendAsync(fileContent, cancellation: ct);
    }
}