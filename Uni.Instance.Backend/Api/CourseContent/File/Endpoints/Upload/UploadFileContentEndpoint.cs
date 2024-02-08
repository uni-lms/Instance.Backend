using FastEndpoints;

using Uni.Instance.Backend.Api.CourseContent.File.Data;
using Uni.Instance.Backend.Api.CourseContent.File.Services;
using Uni.Instance.Backend.Configuration.Swagger;
using Uni.Instance.Backend.Extensions;


namespace Uni.Instance.Backend.Api.CourseContent.File.Endpoints.Upload;

public class UploadFileContentEndpoint(CourseContentFileService service) : Endpoint<UploadFileContentRequest> {
  public override void Configure() {
    Version(2);
    Post("/content/file");
    Roles(CanBeUsedBy.OnlyTutor);
    Options(x => x.WithTags(ApiTags.CourseContentFile.Tag));
    AllowFileUploads();
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(UploadFileContentRequest req, CancellationToken ct) {
    var result = await service.UploadFile(req, ct);
    await this.SendResponseAsync(result, ct);
  }
}