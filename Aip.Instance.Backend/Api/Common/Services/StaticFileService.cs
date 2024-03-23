using System.Globalization;
using System.Security.Cryptography;
using System.Text;

using Aip.Instance.Backend.Api.Content.File.Data;

using Sqids;


namespace Aip.Instance.Backend.Api.Common.Services;

public class StaticFileService(
  IHostEnvironment environment,
  ILogger<StaticFileService> logger
) {
  private readonly string _uploadsPath = Path.Combine(environment.ContentRootPath, "uploads");

  public async Task<string> CalculateChecksumAsync(IFormFile file, CancellationToken ct = default) {
    using var md5 = MD5.Create();
    using var streamReader = new StreamReader(file.OpenReadStream());
    return BitConverter
      .ToString(await md5.ComputeHashAsync(streamReader.BaseStream, ct))
      .Replace("-", "");
  }

  public async Task<FileSaveResult> SaveFileAsync(IFormFile file, CancellationToken ct = default) {
    if (file.Length <= 0) {
      return new FileSaveResult {
        IsSuccess = false,
        Error = "Файл не может быть пустым",
      };
    }

    if (new DriveInfo(Path.GetPathRoot(_uploadsPath) ?? string.Empty).AvailableFreeSpace <= file.Length) {
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

  public string BytesToString(long fileLength) {
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

  public void RemoveFile(string filePath) {
    try {
      System.IO.File.Delete(filePath);
    }
    catch (Exception e) {
      logger.LogWarning("Произошла I/O ошибка: {Error}", e.Message);
    }
  }
}