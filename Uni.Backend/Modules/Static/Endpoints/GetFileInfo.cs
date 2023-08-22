using System.Net.Mime;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using Uni.Backend.Data;
using Uni.Backend.Modules.Static.Contracts;


namespace Uni.Backend.Modules.Static.Endpoints;

public class GetFileInfo : Endpoint<SearchFileRequest, FileResponse> {
  private readonly AppDbContext _db;

  public GetFileInfo(AppDbContext db) {
    _db = db;
  }

  public override void Configure() {
    Version(1);
    Get("/static/{FileId}");
    Options(x => x.WithTags("Static"));
    Description(b => b
      .Produces<FileResponse>(200, MediaTypeNames.Application.Json)
      .ProducesProblemFE(401)
      .ProducesProblemFE(404)
      .ProducesProblemFE(500));
    Summary(x => {
      x.Summary = "Gets information about uploaded file";
      x.Description = "<b>Allowed scopes:</b> Any authorized user";
      x.Responses[200] = "File information fetched successfully";
      x.Responses[401] = "Not authorized";
      x.Responses[404] = "File was not found";
      x.Responses[500] = "Some other error occured";
    });
  }

  public override async Task HandleAsync(SearchFileRequest req, CancellationToken ct) {
    var file = await _db.StaticFiles
      .AsNoTracking()
      .Where(e => e.Id == req.FileId)
      .FirstOrDefaultAsync(ct);

    if (file is null) {
      ThrowError("File was not found", 404);
    }

    await SendAsync(new FileResponse {
      Checksum = file.Checksum!,
      FileId = file.Id,
      VisibleName = file.VisibleName,
    }, cancellation: ct);
  }
}