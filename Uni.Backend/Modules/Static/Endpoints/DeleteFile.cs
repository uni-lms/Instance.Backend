using FastEndpoints;
using Uni.Backend.Data;
using Uni.Backend.Modules.Static.Contracts;

namespace Uni.Backend.Modules.Static.Endpoints;

public class DeleteFile : Endpoint<SearchFileRequest>
{
    private readonly AppDbContext _db;

    public DeleteFile(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Version(1);
        Delete("/static/{FileId}");
        Options(x => x.WithTags("Static"));
        Description(b => b
            .Produces(204)
            .ProducesProblemFE(401)
            .ProducesProblemFE(404)
            .ProducesProblemFE(500));
        Summary(x =>
        {
            x.Summary = "Permanently deletes static file";
            x.Description = "<b>Allowed scopes:</b> Any authorized user";
            x.Responses[204] = "Static file deleted successfully";
            x.Responses[401] = "Not authorized";
            x.Responses[404] = "File was not found";
            x.Responses[500] = "Some other error occured";
        });
    }

    public override async Task HandleAsync(SearchFileRequest req, CancellationToken ct)
    {
        var file = await _db.StaticFiles.FindAsync(new object?[] { req.FileId }, cancellationToken: ct);

        if (file is null)
        {
            ThrowError("File was not found", 404);
        }
        
        File.Delete(file.FilePath);
        _db.StaticFiles.Remove(file);
        await _db.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
    }
}