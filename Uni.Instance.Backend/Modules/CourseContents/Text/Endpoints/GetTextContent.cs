using FastEndpoints;

using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;

using Uni.Backend.Configuration;
using Uni.Backend.Data;
using Uni.Backend.Modules.Common.Contracts;


namespace Uni.Instance.Backend.Modules.CourseContents.Text.Endpoints;

public class GetTextContent : Endpoint<SearchEntityRequest> {
  private readonly AppDbContext _db;

  public GetTextContent(AppDbContext db) {
    _db = db;
  }

  public override void Configure() {
    Version(1);
    Get("/materials/text/{id}");
    Options(x => x.WithTags("Course Materials. Text"));
    Description(b => b
      .ClearDefaultProduces()
      .Produces(200)
      .ProducesProblemFE(401)
      .ProducesProblemFE(403)
      .ProducesProblemFE(404)
      .ProducesProblemFE(500));
    Summary(x => {
      x.Summary = "Gets content of text file";
      x.Description = "<b>Allowed scopes:</b> Any authorized user";
      x.Responses[200] = "Content fetched successfully";
      x.Responses[401] = "Not authorized";
      x.Responses[403] = "Access forbidden";
      x.Responses[404] = "Content was not found";
      x.Responses[500] = "Some other error occured";
    });
  }

  public override async Task HandleAsync(SearchEntityRequest req, CancellationToken ct) {
    var textContent = await _db.TextContents
      .Where(e => e.Id == req.Id)
      .Include(e => e.Course)
      .Include(e => e.Content)
      .FirstOrDefaultAsync(ct);

    if (textContent is null) {
      ThrowError(e => e.Id, "Text content was not found", 404);
    }

    new FileExtensionContentTypeProvider().TryGetContentType(textContent.Content.FileName, out var contentType);

    await SendBytesAsync(
      await File.ReadAllBytesAsync(textContent.Content.FilePath, ct), contentType ?? "",
      cancellation: ct);
  }
}