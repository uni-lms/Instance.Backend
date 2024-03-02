using Aip.Instance.Backend.Api.Common.Services;
using Aip.Instance.Backend.Api.Content.File.Data;
using Aip.Instance.Backend.Data;
using Aip.Instance.Backend.Data.Common;
using Aip.Instance.Backend.Data.Models;

using Ardalis.Result;

using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;

using FileInfo = Aip.Instance.Backend.Api.Content.File.Data.FileInfo;


namespace Aip.Instance.Backend.Api.Content.File.Services;

public class ContentFileService(
  AppDbContext db,
  StaticFileService fileService
) {
  public async Task<Result<UploadFileContentResponse>> UploadFile(UploadFileContentRequest req, CancellationToken ct) {
    var course = await db.Internships.Where(e => e.Id == req.CourseId).FirstOrDefaultAsync(ct);

    if (course is null) {
      return Result.NotFound("Курс не найден");
    }

    var courseSection = await db.Sections.Where(e => e.Id == req.SectionId).FirstOrDefaultAsync(ct);

    if (courseSection is null) {
      return Result.NotFound("Раздел курса не найден");
    }

    var checksum = await fileService.CalculateChecksumAsync(req.Content, ct);

    var existedFile = await db.StaticFiles.Where(e => e.Checksum == checksum).FirstOrDefaultAsync(ct);

    if (existedFile is null) {
      var ioResult = await fileService.SaveFileAsync(req.Content, ct);

      if (ioResult.IsSuccess) {
        existedFile = new StaticFile {
          Id = ioResult.FileId!,
          Checksum = checksum,
          Filename = req.Content.FileName,
          Filepath = ioResult.FilePath,
        };

        await db.StaticFiles.AddAsync(existedFile, ct);
      }
      else {
        return Result.Error($"Не вышло загрузить файл: {ioResult.Error}");
      }
    }

    var content = new FileContent {
      Title = req.Title,
      Internship = course,
      Section = courseSection,
      File = existedFile,
      IsVisibleToInterns = req.IsVisibleToStudents,
    };

    await db.FileContents.AddAsync(content, ct);

    return Result.Success(new UploadFileContentResponse {
      Id = content.Id,
      Title = content.Title,
    });
  }

  public async Task<Result<UploadFileContentResponse>> EditFileAsync(EditFileContentRequest req, CancellationToken ct) {
    var content = await db.FileContents.Where(e => e.Id == req.Id).FirstOrDefaultAsync(ct);

    if (content is null) {
      return Result.NotFound("Файл не был найден");
    }

    if (req.Title is not null) {
      content.Title = req.Title;
    }

    content.IsVisibleToInterns = req.IsVisibleToInterns;

    await db.SaveChangesAsync(ct);

    return Result.Success(new UploadFileContentResponse {
      Id = content.Id,
      Title = content.Title,
    });
  }

  public async Task<Result<UploadFileContentResponse>> DeleteFileAsync(SearchByIdModel req, CancellationToken ct) {
    var content = await db.FileContents.Where(e => e.Id == req.Id).FirstOrDefaultAsync(ct);

    if (content is null) {
      return Result.NotFound("Файл не найден");
    }

    db.Remove(content.File);
    db.Remove(content);

    fileService.RemoveFile(content.File.Filepath!);

    return Result.Success(new UploadFileContentResponse {
      Id = content.Id,
      Title = content.Title,
    });
  }

  public async Task<Result<FileInfo>> GetFileInfoAsync(SearchByIdModel req, CancellationToken ct) {
    var content = await db.FileContents
      .Where(e => e.Id == req.Id)
      .Include(e => e.Internship)
      .Include(e => e.File)
      .FirstOrDefaultAsync(ct);

    if (content is null) {
      return Result.NotFound("Файл не был найден");
    }

    var extension = Path.GetExtension(content.File.Filepath!);
    new FileExtensionContentTypeProvider().TryGetContentType(content.File.Filepath!, out var contentType);
    return Result.Success(new FileInfo {
      Id = content.Id,
      Title = content.Title,
      FileSize = fileService.BytesToString(new System.IO.FileInfo(content.File.Filepath!).Length),
      FileName = content.File.Filename,
      Extension = extension,
      ContentType = contentType!,
    });
  }
}