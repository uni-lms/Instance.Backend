using FastEndpoints;

using Uni.Instance.Backend.Api.Course.Data;
using Uni.Instance.Backend.Api.Course.Services;
using Uni.Instance.Backend.Configuration.Swagger;
using Uni.Instance.Backend.Extensions;


namespace Uni.Instance.Backend.Api.Course.Endpoints.Create;

public class CreateCourseEndpoint(CoursesService service) : Endpoint<CreateCourseRequest, BaseCourseDto> {
  public override void Configure() {
    Version(2);
    Post("/course");
    Options(x => x.WithTags(ApiTags.Courses.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(CreateCourseRequest req, CancellationToken ct) {
    var result = await service.CreateCourseAsync(ValidationFailed, ValidationFailures, User, req, ct);
    await this.SendResponseAsync(result, ct);
  }
}