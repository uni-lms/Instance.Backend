using FastEndpoints;

using Uni.Instance.Backend.Api.Course.Services;
using Uni.Instance.Backend.Configuration.Swagger;
using Uni.Instance.Backend.Extensions;


namespace Uni.Instance.Backend.Api.Course.Endpoints.GetAll;

public class GetAllCoursesEndpoint(CoursesService service) : Endpoint<EmptyRequest> {
  public override void Configure() {
    Version(2);
    Get("/course");
    Roles(CanBeUsedBy.TutorAndAbove);
    Options(x => x.WithTags(ApiTags.Courses.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(EmptyRequest _, CancellationToken ct) {
    var result = await service.GetAllCoursesAsync(ct);
    await this.SendResponseAsync(result, ct);
  }
}