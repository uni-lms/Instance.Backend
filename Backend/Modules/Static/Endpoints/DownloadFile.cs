using Backend.Data;
using Backend.Modules.Static.Contracts;
using FastEndpoints;

namespace Backend.Modules.Static.Endpoints;

public class DownloadFile : Endpoint<SearchFileRequest>
{
    private readonly AppDbContext _db;

    public DownloadFile(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Get("/static/{FileId}/download");
        Options(x => x.WithTags("Static"));
    }

    public override async Task HandleAsync(SearchFileRequest req, CancellationToken ct)
    {
        var file = await _db.StaticFiles.FindAsync(new object?[] { req.FileId }, cancellationToken: ct);

        if (file is null)
        {
            ThrowError("File was not found", 404);
        }

        if (File.Exists(file.FilePath))
        {
            var fileStream = new FileStream(file.FilePath, FileMode.Open);
            await SendStreamAsync(
                fileStream,
                fileName: file.FileName,
                fileLengthBytes: fileStream.Length,
                cancellation: ct
            );
        }
    }
}