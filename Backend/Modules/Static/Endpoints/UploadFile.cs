using Backend.Data;
using Backend.Modules.Static.Contracts;
using Backend.Modules.Static.Services;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Backend.Modules.Static.Endpoints;

public class UploadFile : Endpoint<UploadFileRequest, UploadFileResponse>
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

        var checksum = await _staticService.GetChecksum(req.File);
        var oldFile = await _db.StaticFiles.Where(e => e.Checksum == checksum).FirstOrDefaultAsync(ct);

        if (oldFile is not null)
        {
            await SendAsync(new UploadFileResponse
            {
                Checksum = checksum,
                FileId = oldFile.Id,
                VisibleName = oldFile.VisibleName
            }, cancellation: ct);
            return;
        }
        
        var fileSaveResult = await _staticService.SaveFile(req.File);

        if (!fileSaveResult.IsSuccess)
        {
            ThrowError("File is empty");
        }
        
        var staticFile = new StaticFile
        {
            Id = fileSaveResult.FileId!,
            Checksum = await _staticService.GetChecksum(req.File),
            FileName = req.File.FileName,
            FilePath = fileSaveResult.FilePath!,
            VisibleName = req.VisibleName
        };

        await _db.StaticFiles.AddAsync(staticFile, ct);
        await _db.SaveChangesAsync(ct);

        await SendAsync(new UploadFileResponse
        {
            FileId = staticFile.Id,
            VisibleName = req.VisibleName,
            Checksum = await _staticService.GetChecksum(req.File),
        }, cancellation: ct);
    }
}