using FastEndpoints;

using JetBrains.Annotations;

using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;

using Uni.Backend.Data;
using Uni.Backend.Modules.Common.Contracts;
using Uni.Instance.Backend.Modules.CourseContents.File.Contracts;
using Uni.Instance.Backend.Modules.CourseContents.Text.Contract;


namespace Uni.Instance.Backend.Modules.CourseContents.File.Endpoints;

public class GetFileContentInfo : Endpoint<SearchEntityRequest, GetFileContentInfoResponse> {
  private readonly AppDbContext _db;

  public GetFileContentInfo(AppDbContext db) {
    _db = db;
  }

  public override void Configure() {
    Version(1);
    Get("/materials/file/{id}/info");
    Options(x => x.WithTags("Course Materials. Files"));
    Description(b => b
      .ClearDefaultProduces()
      .Produces(200)
      .ProducesProblemFE(401)
      .ProducesProblemFE(403)
      .ProducesProblemFE(404)
      .ProducesProblemFE(500));
    Summary(x => {
      x.Summary = "Gets some info about file content";
      x.Description = "<b>Allowed scopes:</b> Any authorized user";
      x.Responses[200] = "Content info fetched successfully";
      x.Responses[401] = "Not authorized";
      x.Responses[403] = "Access forbidden";
      x.Responses[404] = "Content was not found";
      x.Responses[500] = "Some other error occured";
    });
  }

  public override async Task HandleAsync(SearchEntityRequest req, CancellationToken ct) {
    var fileContent = await _db.FileContents
      .Where(e => e.Id == req.Id)
      .Include(e => e.Course)
      .Include(e => e.File)
      .FirstOrDefaultAsync(ct);

    if (fileContent is null) {
      ThrowError(e => e.Id, "File content was not found", 404);
    }
    
    new FileExtensionContentTypeProvider().TryGetContentType(fileContent.File.FileName, out var contentType);

    var fileInfo = new FileInfo(fileContent.File.FilePath);
    await SendAsync(new GetFileContentInfoResponse {
      CourseAbbreviation = fileContent.Course.Abbreviation,
      VisibleName = fileContent.File.VisibleName,
      FileSize = fileInfo.Length,
      Extension = fileInfo.Extension.ToUpper(),
      FileId = fileContent.File.Id,
      FileName = fileContent.File.FileName,
      MimeType = contentType!,
    }, cancellation: ct);
  }
}