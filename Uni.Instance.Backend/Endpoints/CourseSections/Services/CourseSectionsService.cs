using Ardalis.Result;

using Microsoft.EntityFrameworkCore;

using Uni.Instance.Backend.Data;
using Uni.Instance.Backend.Endpoints.CourseSections.Data;


namespace Uni.Instance.Backend.Endpoints.CourseSections.Services;

public class CourseSectionsService(AppDbContext db) {
  public async Task<Result<GetAllCourseSectionsResponse>> GetAll() {
    var sections = await db.CourseSections.ToListAsync();

    var result = new GetAllCourseSectionsResponse {
      Sections = sections,
    };

    return Result.Success(result);
  }
}