using Ardalis.Result;

using Microsoft.EntityFrameworkCore;

using Uni.Instance.Backend.Api.CourseSections.Data;
using Uni.Instance.Backend.Data;


namespace Uni.Instance.Backend.Api.CourseSections.Services;

public class CourseSectionsService(AppDbContext db) {
  public async Task<Result<GetAllCourseSectionsResponse>> GetAll() {
    var sections = await db.CourseSections.ToListAsync();

    var result = new GetAllCourseSectionsResponse {
      Sections = sections,
    };

    return Result.Success(result);
  }
}