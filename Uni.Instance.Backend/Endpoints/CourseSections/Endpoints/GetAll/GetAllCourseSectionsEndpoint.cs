using FastEndpoints;

using Uni.Instance.Backend.Configuration.Swagger;
using Uni.Instance.Backend.Endpoints.CourseSections.Data;
using Uni.Instance.Backend.Endpoints.CourseSections.Services;
using Uni.Instance.Backend.Extensions;


namespace Uni.Instance.Backend.Endpoints.CourseSections.Endpoints.GetAll;

public class GetAllCourseSectionsEndpoint(CourseSectionsService service)
  : Endpoint<EmptyRequest, GetAllCourseSectionsResponse> {
  public override void Configure() {
    Version(2);
    Get("/course-sections");
    Options(x => x.WithTags(ApiTags.CourseSections.Tag));
  }

  public override async Task HandleAsync(EmptyRequest req, CancellationToken ct) {
    var result = await service.GetAll();

    await this.SendResponseAsync(result, ct);
  }
}