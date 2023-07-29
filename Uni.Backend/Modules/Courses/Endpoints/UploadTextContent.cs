using System.Net.Mime;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Uni.Backend.Configuration;
using Uni.Backend.Data;
using Uni.Backend.Modules.CourseContents.Text.Contract;
using Uni.Backend.Modules.Courses.Contract;
using Uni.Backend.Modules.Static.Contracts;
using Uni.Backend.Modules.Static.Services;

namespace Uni.Backend.Modules.Courses.Endpoints;

public class UploadTextContent : Endpoint<UploadTextContentRequest, TextContent>
{
    private readonly AppDbContext _db;
    private readonly StaticService _staticService;

    public UploadTextContent(AppDbContext db, StaticService staticService)
    {
        _db = db;
        _staticService = staticService;
    }

    public override void Configure()
    {
        Version(1);
        AllowFileUploads();
        Roles(UserRoles.MinimumRequired(UserRoles.Tutor));
        Post("/courses/{CourseId}/text");
        Options(x => x.WithTags("Courses"));
        Description(b => b
            .Produces<List<CourseDto>>(201, MediaTypeNames.Application.Json)
            .ProducesProblemFE(401)
            .ProducesProblemFE(403)
            .ProducesProblemFE(404)
            .ProducesProblemFE(409)
            .ProducesProblemFE(500));
        Summary(x =>
        {
            x.Summary = "Uploads text content to the course";
            x.Description = """
                               <b>Allowed scopes:</b> Tutor, Administrator
                            """;
            x.Responses[201] = "Content uploaded successfully";
            x.Responses[401] = "Not authorized";
            x.Responses[403] = "Access forbidden";
            x.Responses[404] = "Some related entity was not found";
            x.Responses[409] = "Block doesn't enabled on the course";
            x.Responses[500] = "Some other error occured";
        });
    }

    public override async Task HandleAsync(UploadTextContentRequest req, CancellationToken ct)
    {
        var course = await _db.Courses
            .Where(e => e.Id == req.CourseId)
            .Include(e => e.Blocks)
            .FirstOrDefaultAsync(ct);

        if (course is null)
        {
            ThrowError("Course was not found", 404);
        }

        var block = await _db.CourseBlocks.FindAsync(new object?[] { req.BlockId }, ct);

        if (block is null)
        {
            ThrowError("Course block was not found");
        }

        if (!course.Blocks.Contains(block))
        {
            ThrowError("This block wasn't enabled in the course", 409);
        }

        var result = await _staticService.SaveFile(req.Content, ct);

        if (result.IsSuccess)
        {

            var file = new StaticFile
            {
                Id = result.FileId!,
                Checksum = await _staticService.GetChecksum(req.Content, ct),
                FileName = req.Content.FileName,
                FilePath = result.FilePath!,
                VisibleName = req.VisibleName
            };

            await _db.StaticFiles.AddAsync(file, ct);
            
            var content = new TextContent
            {
                Course = course,
                Block = block,
                IsVisibleToStudents = req.IsVisibleToStudents,
                Content = file
            };

            await _db.TextContents.AddAsync(content, ct);
            await _db.SaveChangesAsync(ct);

            await SendCreatedAtAsync($"/courses/{req.CourseId}/text", null, content, cancellation: ct);
        }
        else
        {
            ThrowError("Unable to save file", 500);
        }
    }
}