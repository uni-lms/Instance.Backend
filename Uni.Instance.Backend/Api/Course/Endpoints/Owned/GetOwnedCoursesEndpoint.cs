using FastEndpoints;

using Uni.Instance.Backend.Api.Course.Services;
using Uni.Instance.Backend.Configuration.Swagger;
using Uni.Instance.Backend.Extensions;


namespace Uni.Instance.Backend.Api.Course.Endpoints.Owned;

public class GetOwnedCoursesEndpoint(CoursesService service) : Endpoint<EmptyRequest> {
  public override void Configure() {
    Version(2);
    Post("/courses/owned");
    Roles(CanBeUsedBy.OnlyTutor);
    Options(x => x.WithTags(ApiTags.Courses.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(EmptyRequest req, CancellationToken ct) {
    var result = await service.GetOwnedCoursesAsync(User, ct);
    await this.SendResponseAsync(result, ct);
  }
}