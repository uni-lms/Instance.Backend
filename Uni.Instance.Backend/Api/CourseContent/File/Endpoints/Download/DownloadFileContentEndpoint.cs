using Ardalis.Result;

using FastEndpoints;

using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;

using Uni.Instance.Backend.Configuration.Swagger;
using Uni.Instance.Backend.Data;
using Uni.Instance.Backend.Data.Common;
using Uni.Instance.Backend.Extensions;


namespace Uni.Instance.Backend.Api.CourseContent.File.Endpoints.Download;

public class DownloadFileContentEndpoint(AppDbContext db) : Endpoint<SearchByIdModel> {
  public override void Configure() {
    Version(2);
    Put("/content/file/{id}/download");
    Options(x => x.WithTags(ApiTags.CourseContentFile.Tag));
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