using Aip.Instance.Backend.Api.Content.Text.Data;
using Aip.Instance.Backend.Data;
using Aip.Instance.Backend.Data.Common;
using Aip.Instance.Backend.Data.Models;

using Ardalis.Result;

using Microsoft.EntityFrameworkCore;


namespace Aip.Instance.Backend.Api.Content.Text.Services;

public class TextContentService(AppDbContext db) {
  public async Task<Result<CreateTextContentResponse>> CreateTextContent(
    CreateTextContentRequest req,
    CancellationToken ct
  ) {
    var internship = await db.Internships
      .Where(e => e.Id == req.InternshipId)
      .FirstOrDefaultAsync(ct);

    if (internship is null) {
      return Result.NotFound(nameof(internship));
    }

    var section = await db.Sections
      .Where(e => e.Id == req.SectionId)
      .FirstOrDefaultAsync(ct);

    if (section is null) {
      return Result.NotFound(nameof(section));
    }

    var content = new TextContent {
      Internship = internship,
      Section = section,
      IsVisibleToStudents = req.IsVisibleToStudents,
      Text = req.Text,
    };

    await db.TextContents.AddAsync(content, ct);
    await db.SaveChangesAsync(ct);

    return Result.Success(new CreateTextContentResponse {
      Id = content.Id,
    });
  }

  public async Task<Result<SearchByIdModel>> DeleteTextContent(SearchByIdModel req, CancellationToken ct) {
    var content = await db.TextContents.FirstOrDefaultAsync(e => e.Id == req.Id, ct);

    if (content is null) {
      return Result.NotFound(nameof(content));
    }

    db.TextContents.Remove(content);
    await db.SaveChangesAsync(ct);

    return Result.Success(req);
  }
}