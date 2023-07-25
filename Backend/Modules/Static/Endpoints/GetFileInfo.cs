using Backend.Data;
using Backend.Modules.Static.Contracts;
using FastEndpoints;

namespace Backend.Modules.Static.Endpoints;

public class GetFileInfo : Endpoint<SearchFileRequest, FileResponse>
{
    private readonly AppDbContext _db;

    public GetFileInfo(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Get("/static/{FileId}");
        Options(x => x.WithTags("Static"));
    }

    public override async Task HandleAsync(SearchFileRequest req, CancellationToken ct)
    {
        var file = await _db.StaticFiles.FindAsync(new object?[] { req.FileId }, cancellationToken: ct);

        if (file is null)
        {
            ThrowError("File was not found", 404);
        }

        await SendAsync(new FileResponse
        {
            Checksum = file.Checksum,
            FileId = file.Id,
            VisibleName = file.VisibleName
        }, cancellation: ct);
    }
}