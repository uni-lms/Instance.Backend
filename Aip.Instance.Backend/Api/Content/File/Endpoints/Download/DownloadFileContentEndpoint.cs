using Aip.Instance.Backend.Configuration.Swagger;
using Aip.Instance.Backend.Data;
using Aip.Instance.Backend.Data.Common;
using Aip.Instance.Backend.Extensions;

using Ardalis.Result;

using FastEndpoints;

using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;


namespace Aip.Instance.Backend.Api.Content.File.Endpoints.Download;

public class DownloadFileContentEndpoint(AppDbContext db) : Endpoint<SearchByIdModel> {
  public override void Configure() {
    Version(2);
    Get("/content/file/{id}/download");
    Options(x => x.WithTags(ApiTags.FileContent.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(SearchByIdModel req, CancellationToken ct) {
    var content = await db.FileContents
      .Where(e => e.Id == req.Id)
      .Include(e => e.File)
      .FirstOrDefaultAsync(ct);

    if (content is null) {
      await this.SendResponseAsync(Result.NotFound("Файл не был найден"), ct);
      return;
    }

    var filepath = content.File.Filepath;
    if (System.IO.File.Exists(filepath!)) {
      var fileStream = new FileStream(filepath!, FileMode.Open);

      new FileExtensionContentTypeProvider().TryGetContentType(content.File.Filepath!, out var contentType);
      await SendStreamAsync(fileStream, fileName: content.File.Filename, fileLengthBytes: fileStream.Length,
        contentType: contentType!, cancellation: ct);
    }
  }
}