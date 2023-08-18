using System.Net.Mime;
using System.Security.Claims;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using Uni.Backend.Configuration;
using Uni.Backend.Data;
using Uni.Backend.Modules.CourseContents.Common.Contracts;
using Uni.Backend.Modules.CourseContents.File.Contracts;
using Uni.Backend.Modules.Static.Contracts;
using Uni.Backend.Modules.Static.Services;


namespace Uni.Backend.Modules.CourseContents.File.Endpoints;

public class UploadFileContent : Endpoint<UploadContentRequest, FileContent> {
  private readonly AppDbContext _db;
  private readonly StaticService _staticService;

  public UploadFileContent(AppDbContext db, StaticService staticService) {
    _db = db;
    _staticService = staticService;
  }

  public override void Configure() {
    Version(1);
    AllowFileUploads();
    Roles(UserRoles.MinimumRequired(UserRoles.Tutor));
    Post("/courses/{CourseId}/file");
    Options(x => x.WithTags("Course Materials"));
    Description(b => b
      .ClearDefaultProduces()
      .Produces<FileContent>(201, MediaTypeNames.Application.Json)
      .ProducesProblemFE(401)
      .ProducesProblemFE(403)
      .ProducesProblemFE(404)
      .ProducesProblemFE(409)
      .ProducesProblemFE(500));
    Summary(x => {
      x.Summary = "Uploads file content to the course";
      x.Description = "<b>Allowed scopes:</b> Any Administrator, Tutor who ownes the course";
      x.Responses[201] = "Content uploaded successfully";
      x.Responses[401] = "Not authorized";
      x.Responses[403] = "Access forbidden";
      x.Responses[404] = "Some related entity was not found";
      x.Responses[409] = "Block doesn't enabled on the course";
      x.Responses[500] = "Some other error occured";
    });
  }

  public override async Task HandleAsync(UploadContentRequest req, CancellationToken ct) {
    var course = await _db.Courses
      .Where(e => e.Id == req.CourseId)
      .Include(e => e.Blocks)
      .FirstOrDefaultAsync(ct);

    if (course is null) {
      ThrowError("Course was not found", 404);
    }

    var user = await _db.Users.AsNoTracking().Where(e => e.Email == User.Identity!.Name).FirstAsync(ct);

    if (User.HasClaim(ClaimTypes.Role, UserRoles.Tutor) && !course.Owners.Contains(user)) {
      ThrowError(_ => User, "Access forbidden", 403);
    }

    var block = await _db.CourseBlocks
      .AsNoTracking()
      .Where(e => e.Id == req.BlockId)
      .FirstOrDefaultAsync(ct);

    if (block is null) {
      ThrowError("Course block was not found");
    }

    if (!course.Blocks.Contains(block)) {
      ThrowError("This block wasn't enabled in the course", 409);
    }

    var result = await _staticService.SaveFile(req.Content, ct);

    if (result.IsSuccess) {
      var file = new StaticFile {
        Id = result.FileId!,
        Checksum = await StaticService.GetChecksum(req.Content, ct),
        FileName = req.Content.FileName,
        FilePath = result.FilePath!,
        VisibleName = req.VisibleName,
      };

      await _db.StaticFiles.AddAsync(file, ct);

      var content = new FileContent {
        Course = course,
        Block = block,
        IsVisibleToStudents = req.IsVisibleToStudents,
        File = file,
      };

      await _db.FileContents.AddAsync(content, ct);
      await _db.SaveChangesAsync(ct);

      await SendCreatedAtAsync($"/courses/{req.CourseId}/file", null, content, cancellation: ct);
    }
    else {
      ThrowError("Unable to save file", 500);
    }
  }
}