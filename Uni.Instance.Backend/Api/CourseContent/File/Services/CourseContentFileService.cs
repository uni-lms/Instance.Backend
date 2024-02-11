using System.Security.Cryptography;
using System.Text;

using Ardalis.Result;

using Microsoft.EntityFrameworkCore;

using Sqids;

using Uni.Instance.Backend.Api.CourseContent.File.Data;
using Uni.Instance.Backend.Data;
using Uni.Instance.Backend.Data.Common;
using Uni.Instance.Backend.Data.Models;

using FileInfo = Uni.Instance.Backend.Api.CourseContent.File.Data.FileInfo;


namespace Uni.Instance.Backend.Api.CourseContent.File.Services;

public class CourseContentFileService(
  AppDbContext db,
  IHostEnvironment environment,
  ILogger<CourseContentFileService> logger
) {
  private readonly string _uploadsPath = Path.Combine(environment.ContentRootPath, "uploads");

  public async Task<Result<UploadFileContentResponse>> UploadFile(UploadFileContentRequest req, CancellationToken ct) {
    var course = await db.Courses.Where(e => e.Id == req.CourseId).FirstOrDefaultAsync(ct);

    if (course is null) {
      return Result.NotFound("Курс не найден");
    }

    var courseSection = await db.CourseSections.Where(e => e.Id == req.SectionId).FirstOrDefaultAsync(ct);

    if (courseSection is null) {
      return Result.NotFound("Раздел курса не найден");
    }

    var checksum = await CalculateChecksumAsync(req.Content, ct);

    var existedFile = await db.StaticFiles.Where(e => e.Checksum == checksum).FirstOrDefaultAsync(ct);

    if (existedFile is null) {
      var ioResult = await SaveFileAsync(req.Content, ct);

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

    var content = new CourseContentFile {
      Title = req.Title,
      Course = course,
      Section = courseSection,
      File = existedFile,
      IsVisibleToStudents = req.IsVisibleToStudents,
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

    content.IsVisibleToStudents = req.IsVisibleToStudents;

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

    try {
      System.IO.File.Delete(content.File.Filepath!);
    }
    catch (Exception e) {
      logger.LogWarning("Произошла I/O ошибка: {Error}", e.Message);
    }

    return Result.Success(new UploadFileContentResponse {
      Id = content.Id,
      Title = content.Title,
    });
  }

  private async Task<string> CalculateChecksumAsync(IFormFile file, CancellationToken ct = default) {
    using var md5 = MD5.Create();
    using var streamReader = new StreamReader(file.OpenReadStream());
    return BitConverter
      .ToString(await md5.ComputeHashAsync(streamReader.BaseStream, ct))
      .Replace("-", "");
  }

  private async Task<FileSaveResult> SaveFileAsync(IFormFile file, CancellationToken ct = default) {
    if (file.Length <= 0) {
      return new FileSaveResult {
        IsSuccess = false,
        Error = "Файл не может быть пустым",
      };
    }

    if (new DriveInfo(_uploadsPath).AvailableFreeSpace <= file.Length) {
      return new FileSaveResult {
        IsSuccess = false,
        Error = "На диске недостаточно свободного места",
      };
    }

    Directory.CreateDirectory(_uploadsPath);

    var fileId = new SqidsEncoder<long>(new SqidsOptions {
      MinLength = 7,
    }).Encode(file.Length);
    var fileName = new StringBuilder(fileId)
      .Append(Path.GetExtension(file.FileName))
      .ToString();
    var path = Path.Combine(_uploadsPath, fileName);
    await using var stream = new FileStream(path, FileMode.Create);
    await file.CopyToAsync(stream, ct);

    return new FileSaveResult {
      IsSuccess = true,
      FileId = fileId,
      FilePath = path,
    };
  }

  public static string BytesToString(long fileLength) {
    string[] suf = ["Б", "КБ", "МБ", "ГБ", "ТБ"];

    if (fileLength == 0) {
      return $"0 {suf[0]}";
    }

    var bytes = Math.Abs(fileLength);
    var place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
    var num = Math.Round(bytes / Math.Pow(1024, place), 1);
    return $"{Math.Sign(fileLength) * num} {suf[place]}";
  }
}