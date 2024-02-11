using FastEndpoints;

using Uni.Instance.Backend.Api.CourseContent.File.Services;
using Uni.Instance.Backend.Configuration.Swagger;
using Uni.Instance.Backend.Data.Common;
using Uni.Instance.Backend.Extensions;


namespace Uni.Instance.Backend.Api.CourseContent.File.Endpoints.Delete;

public class DeleteFileContentEndpoint(CourseContentFileService service) : Endpoint<SearchByIdModel> {
  public override void Configure() {
    Version(2);
    Delete("/content/file/{id}");
    Roles(CanBeUsedBy.OnlyTutor);
    Options(x => x.WithTags(ApiTags.CourseContentFile.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(SearchByIdModel req, CancellationToken ct) {
    var result = await service.DeleteFileAsync(req, ct);
    await this.SendResponseAsync(result, ct);
  }
}