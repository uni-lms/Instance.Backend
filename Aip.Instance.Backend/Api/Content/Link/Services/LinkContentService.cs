using Aip.Instance.Backend.Api.Content.Link.Data;
using Aip.Instance.Backend.Data;
using Aip.Instance.Backend.Data.Models;

using Ardalis.Result;

using Microsoft.EntityFrameworkCore;


namespace Aip.Instance.Backend.Api.Content.Link.Services;

public class LinkContentService(AppDbContext db) {
  public async Task<Result<CreateLinkContentResponse>> CreateLinkContent(
    CreateLinkContentRequest req,
    CancellationToken ct
  ) {
    var internship = await db.Internships.FirstOrDefaultAsync(e => e.Id == req.InternshipId, ct);

    if (internship is null) {
      return Result.NotFound(nameof(internship));
    }

    var section = await db.Sections.FirstOrDefaultAsync(e => e.Id == req.SectionId, ct);

    if (section is null) {
      return Result.NotFound(nameof(section));
    }

    var content = new LinkContent {
      Link = req.Link,
      Internship = internship,
      Section = section,
      IsVisibleToStudents = req.IsVisibleToStudents,
      Title = req.Title,
    };

    await db.LinkContents.AddAsync(content, ct);
    await db.SaveChangesAsync(ct);

    return Result.Success(new CreateLinkContentResponse {
      Id = content.Id,
    });
  }
}