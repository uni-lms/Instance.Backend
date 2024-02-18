using Aip.Instance.Backend.Api.Sections.Data;
using Aip.Instance.Backend.Data;

using Ardalis.Result;

using Microsoft.EntityFrameworkCore;


namespace Aip.Instance.Backend.Api.Sections.Services;

public class CourseSectionsService(AppDbContext db) {
  public async Task<Result<GetAllCourseSectionsResponse>> GetAll() {
    var sections = await db.Sections.ToListAsync();

    var result = new GetAllCourseSectionsResponse {
      Sections = sections,
    };

    return Result.Success(result);
  }
}