﻿using FastEndpoints;
using Microsoft.EntityFrameworkCore;
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
        Delete("/static/{FileId}");
        Options(x => x.WithTags("Static"));
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