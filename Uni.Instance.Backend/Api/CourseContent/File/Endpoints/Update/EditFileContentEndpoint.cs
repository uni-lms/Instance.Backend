using FastEndpoints;

using Uni.Instance.Backend.Api.CourseContent.File.Data;
using Uni.Instance.Backend.Api.CourseContent.File.Services;
using Uni.Instance.Backend.Configuration.Swagger;
using Uni.Instance.Backend.Extensions;


namespace Uni.Instance.Backend.Api.CourseContent.File.Endpoints.Update;

public class EditFileContentEndpoint(CourseContentFileService service) : Endpoint<EditFileContentRequest> {
  public override void Configure() {
    Version(2);
    Put("/content/file/{id}");
    Roles(CanBeUsedBy.OnlyTutor);
    Options(x => x.WithTags(ApiTags.CourseContentFile.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(EditFileContentRequest req, CancellationToken ct) {
    var result = await service.EditFileAsync(req, ct);
    await this.SendResponseAsync(result, ct);
  }
}