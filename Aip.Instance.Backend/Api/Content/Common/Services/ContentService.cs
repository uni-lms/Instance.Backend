using System.Security.Claims;

using Aip.Instance.Backend.Api.Content.Common.Data;
using Aip.Instance.Backend.Data;
using Aip.Instance.Backend.Data.Common;

using Ardalis.Result;

using FastEndpoints.Security;

using Microsoft.EntityFrameworkCore;


namespace Aip.Instance.Backend.Api.Content.Common.Services;

public class ContentService(AppDbContext db) {
  public async Task<Result<Data.Content>> GetFlowContent(
    SearchByIdModel req,
    ClaimsPrincipal user,
    CancellationToken ct
  ) {
    var email = user.ClaimValue(ClaimTypes.Name);

    if (email is null) {
      return Result.Unauthorized();
    }

    var userInfo = await db.Users.Where(e => e.Email == email).FirstOrDefaultAsync(ct);

    if (userInfo is null) {
      return Result.Unauthorized();
    }

    var internship = await db.Internships.Where(e => e.Id == req.Id).Include(e => e.InternshipUserRoles)
      .FirstOrDefaultAsync(ct);

    if (internship is null) {
      return Result.NotFound();
    }

    var roleInfo = internship.InternshipUserRoles.Where(e => e.User == userInfo).ToList();

    if (roleInfo.Count == 0) {
      return Result.Forbidden();
    }

    var isIntern = roleInfo[0].Role.Name == "Intern";

    var fileContents = await db.FileContents
      .Where(e => e.Internship == internship && !isIntern || (isIntern && e.IsVisibleToInterns))
      .GroupBy(e => e.Section.Name)
      .Select(g => new ContentSection {
        Name = g.Key,
        Items = g.Select(e => new FileContentItem {
          Id = e.Id,
          Title = e.Title,
        }),
      }).ToListAsync(ct);

    var textContents = await db.TextContents
      .Where(e => e.Internship == internship && !isIntern || (isIntern && e.IsVisibleToInterns))
      .GroupBy(e => e.Section.Name)
      .Select(g => new ContentSection {
        Name = g.Key,
        Items = g.Select(e => new TextContentItem {
          Id = e.Id,
          Text = e.Text,
        }),
      }).ToListAsync(ct);

    var linkContents = await db.LinkContents
      .Where(e => e.Internship == internship && !isIntern || (isIntern && e.IsVisibleToInterns))
      .GroupBy(e => e.Section.Name)
      .Select(g => new ContentSection {
        Name = g.Key,
        Items = g.Select(e => new LinkContentItem {
          Id = e.Id,
          Link = e.Link,
        }),
      }).ToListAsync(ct);

    var assgnmentContents = await db.Assignments
      .Where(e => e.Internship == internship && !isIntern || (isIntern && e.IsVisibleToInterns))
      .GroupBy(e => e.Section.Name)
      .Select(g => new ContentSection {
        Name = g.Key,
        Items = g.Select(e => new AssignmentContentItem {
          Id = e.Id,
          Title = e.Title,
        }),
      }).ToListAsync(ct);

    var contents = fileContents
      .Concat(textContents)
      .Concat(linkContents)
      .Concat(assgnmentContents)
      .GroupBy(section => section.Name)
      .Select(grouped => new ContentSection {
        Name = grouped.Key,
        Items = grouped.SelectMany(g => g.Items).ToList(),
      })
      .ToList();

    return Result.Success(new Data.Content {
      Title = internship.Name,
      Sections = contents,
    });
  }
}