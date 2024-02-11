using FastEndpoints;

using Uni.Instance.Backend.Api.CourseContent.File.Services;
using Uni.Instance.Backend.Configuration.Swagger;
using Uni.Instance.Backend.Data.Common;


namespace Uni.Instance.Backend.Api.CourseContent.File.Endpoints.Get;

public class GetFileContentInfoEndpoint(CourseContentFileService service) : Endpoint<SearchByIdModel> {
  public override void Configure() {
    Version(2);
    Get("/content/file/{id}");
    Options(x => x.WithTags(ApiTags.CourseContentFile.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(SearchByIdModel req, CancellationToken ct) {
    var result = await service.GetFileInfoAsync(req, ct);
  }
}