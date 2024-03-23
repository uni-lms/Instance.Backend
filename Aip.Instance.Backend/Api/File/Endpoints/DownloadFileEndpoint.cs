using Aip.Instance.Backend.Api.File.Data;
using Aip.Instance.Backend.Configuration.Swagger;
using Aip.Instance.Backend.Data;
using Aip.Instance.Backend.Extensions;

using Ardalis.Result;

using FastEndpoints;

using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;


namespace Aip.Instance.Backend.Api.File.Endpoints;

public class DownloadFileEndpoint(AppDbContext db) : Endpoint<SearchByFileId> {
  public override void Configure() {
    Version(2);
    Post("/file/{id}/download");
    Options(x => x.WithTags(ApiTags.FileContent.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(SearchByFileId req, CancellationToken ct) {
    var content = await db.StaticFiles
      .Where(e => e.Id == req.Id)
      .FirstOrDefaultAsync(ct);

    if (content is null) {
      await this.SendResponseAsync(Result.NotFound("Файл не был найден"), ct);
      return;
    }

    var filepath = content.Filepath;
    if (System.IO.File.Exists(filepath!)) {
      var fileStream = new FileStream(filepath!, FileMode.Open);

      new FileExtensionContentTypeProvider().TryGetContentType(content.Filepath!, out var contentType);
      await SendStreamAsync(fileStream, fileName: content.Filename, fileLengthBytes: fileStream.Length,
        contentType: contentType!, cancellation: ct);
    }
  }
}