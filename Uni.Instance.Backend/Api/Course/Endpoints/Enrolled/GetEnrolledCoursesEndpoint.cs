using FastEndpoints;

using Uni.Instance.Backend.Api.Course.Data;
using Uni.Instance.Backend.Api.Course.Services;
using Uni.Instance.Backend.Configuration.Swagger;
using Uni.Instance.Backend.Extensions;


namespace Uni.Instance.Backend.Api.Course.Endpoints.Enrolled;

public class GetEnrolledCoursesEndpoint(CoursesService service) : Endpoint<EnrolledCoursesFilterRequest> {
  public override void Configure() {
    Version(2);
    Post("/courses/enrolled");
    Roles(CanBeUsedBy.OnlyStudent);
    Options(x => x.WithTags(ApiTags.Courses.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(EnrolledCoursesFilterRequest req, CancellationToken ct) {
    var result = await service.GetEnrolledCourses(User, req, ct);
    await this.SendResponseAsync(result, ct);
  }
}