using FastEndpoints;

using Uni.Instance.Backend.Api.Course.Data;
using Uni.Instance.Backend.Api.Course.Services;
using Uni.Instance.Backend.Configuration.Swagger;
using Uni.Instance.Backend.Data.Common;
using Uni.Instance.Backend.Extensions;


namespace Uni.Instance.Backend.Api.Course.Endpoints.Delete;

public class DeleteCourseEndpoint(CoursesService service) : Endpoint<SearchByIdModel, BaseCourseDto> {
  public override void Configure() {
    Version(2);
    Delete("/courses/{id}");
    Roles(CanBeUsedBy.TutorAndAbove);
    Options(x => x.WithTags(ApiTags.Courses.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(SearchByIdModel req, CancellationToken ct) {
    var result = await service.DeleteCourseAsync(req, ct);
    await this.SendResponseAsync(result, ct);
  }
}