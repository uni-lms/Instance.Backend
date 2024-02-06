using FastEndpoints;

using Uni.Instance.Backend.Api.Course.Data;
using Uni.Instance.Backend.Api.Course.Services;
using Uni.Instance.Backend.Configuration.Swagger;
using Uni.Instance.Backend.Extensions;


namespace Uni.Instance.Backend.Api.Course.Endpoints.AddOwnerToCourse;

public class AddOwnerToCourseEndpoint(CoursesService service) : Endpoint<ListOfGuidsRequest> {
  public override void Configure() {
    Version(2);
    Post("/courses/add-owner");
    Roles(CanBeUsedBy.OnlyTutor);
    Options(x => x.WithTags(ApiTags.Courses.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(ListOfGuidsRequest req, CancellationToken ct) {
    var result = await service.AddOwnerToCourse(req, ct);
    await this.SendResponseAsync(result, ct);
  }
}