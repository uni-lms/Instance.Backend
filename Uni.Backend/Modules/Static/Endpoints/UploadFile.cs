using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Uni.Backend.Data;
using Uni.Backend.Modules.Static.Contracts;
using Uni.Backend.Modules.Static.Services;

namespace Uni.Backend.Modules.Static.Endpoints;

public class UploadFile : Endpoint<UploadFileRequest, FileResponse>
{
    private readonly StaticService _staticService;
    private readonly AppDbContext _db;

    public UploadFile(StaticService staticService, AppDbContext db)
    {
        _staticService = staticService;
        _db = db;
    }

    public override void Configure()
    {
        Post("/static");
        AllowFileUploads();
        Options(x => x.WithTags("Static"));
    }

    public override async Task HandleAsync(UploadFileRequest req, CancellationToken ct)
    {

        var checksum = await _staticService.GetChecksum(req.File, ct);
        var oldFile = await _db.StaticFiles.Where(e => e.Checksum == checksum).FirstOrDefaultAsync(ct);

        if (oldFile is not null)
        {
            await SendAsync(new FileResponse
            {
                Checksum = checksum,
                FileId = oldFile.Id,
                VisibleName = oldFile.VisibleName
            }, cancellation: ct);
            return;
        }
        
        var fileSaveResult = await _staticService.SaveFile(req.File, ct);

        if (!fileSaveResult.IsSuccess)
        {
            ThrowError("File is empty");
        }

        var staticFile = new StaticFile
        {
            Id = fileSaveResult.FileId!,
            Checksum = checksum,
            FileName = req.File.FileName,
            FilePath = fileSaveResult.FilePath!,
            VisibleName = req.VisibleName
        };

        await _db.StaticFiles.AddAsync(staticFile, ct);
        await _db.SaveChangesAsync(ct);

        await SendAsync(new FileResponse
        {
            FileId = staticFile.Id,
            VisibleName = req.VisibleName,
            Checksum = checksum,
        }, cancellation: ct);
    }
}