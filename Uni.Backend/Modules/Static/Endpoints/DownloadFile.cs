using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using Uni.Backend.Data;
using Uni.Backend.Modules.Static.Contracts;


namespace Uni.Backend.Modules.Static.Endpoints;

public class DownloadFile : Endpoint<SearchFileRequest> {
  private readonly AppDbContext _db;

  public DownloadFile(AppDbContext db) {
    _db = db;
  }

  public override void Configure() {
    Version(1);
    Get("/static/{FileId}/download");
    Options(x => x.WithTags("Static"));
    Description(b => b
      .Produces<IFormFile>()
      .ProducesProblemFE(401)
      .ProducesProblemFE(404)
      .ProducesProblemFE(500));
    Summary(x => {
      x.Summary = "Downloads static file";
      x.Description = "<b>Allowed scopes:</b> Any authorized user";
      x.Responses[200] = "File exposed successfully";
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

    if (File.Exists(file.FilePath)) {
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