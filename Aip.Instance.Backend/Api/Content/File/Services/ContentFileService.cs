using System.Globalization;
using System.Security.Cryptography;
using System.Text;

using Aip.Instance.Backend.Api.Content.File.Data;
using Aip.Instance.Backend.Data;
using Aip.Instance.Backend.Data.Common;
using Aip.Instance.Backend.Data.Models;

using Ardalis.Result;

using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;

using Sqids;

using FileInfo = Aip.Instance.Backend.Api.Content.File.Data.FileInfo;


namespace Aip.Instance.Backend.Api.Content.File.Services;

public class ContentFileService(
  AppDbContext db,
  IHostEnvironment environment,
  ILogger<ContentFileService> logger
) {
  private readonly string _uploadsPath = Path.Combine(environment.ContentRootPath, "uploads");

  public async Task<Result<UploadFileContentResponse>> UploadFile(UploadFileContentRequest req, CancellationToken ct) {
    var course = await db.Internships.Where(e => e.Id == req.CourseId).FirstOrDefaultAsync(ct);

    if (course is null) {
      return Result.NotFound("Курс не найден");
    }

    var courseSection = await db.Sections.Where(e => e.Id == req.SectionId).FirstOrDefaultAsync(ct);

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

    var content = new FileContent {
      Title = req.Title,
      Internship = course,
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

    content.IsVisibleToStudents = req.IsVisibleToInterns;

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
      FileSize = BytesToString(new System.IO.FileInfo(content.File.Filepath!).Length),
      FileName = content.File.Filename,
      Extension = extension,
      ContentType = contentType!,
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
    var result = (Math.Sign(fileLength) * num).ToString(CultureInfo.InvariantCulture);
    return $"{result} {suf[place]}";
  }
}