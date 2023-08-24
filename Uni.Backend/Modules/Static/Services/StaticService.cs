using System.Security.Cryptography;
using System.Text;

using Uni.Backend.Modules.Static.Contracts;


namespace Uni.Backend.Modules.Static.Services;

public class StaticService {
  private readonly string _uploadsPath;

  public StaticService(IHostEnvironment hostEnvironment) {
    _uploadsPath = Path.Combine(hostEnvironment.ContentRootPath, "uploads");
  }

  public async Task<FileSaveResult> SaveFile(IFormFile file, CancellationToken ct = default) {
    if (file.Length <= 0) {
      return new FileSaveResult {
        IsSuccess = false,
      };
    }

    Directory.CreateDirectory(_uploadsPath);
    var fileId = ShortId.FromGuid(Guid.NewGuid());
    var fileName = new StringBuilder(fileId)
      .Append(Path.GetExtension(file.FileName))
      .ToString();
    var path = Path.Combine(_uploadsPath, fileName);
    await using var fileStream = new FileStream(path, FileMode.Create);
    await file.CopyToAsync(fileStream, cancellationToken: ct);

    return new FileSaveResult {
      IsSuccess = true,
      FileId = fileId,
      FilePath = path,
    };
  }

  public static async Task<string> GetChecksum(IFormFile file, CancellationToken ct = default) {
    using var md5 = MD5.Create();
    using var streamReader = new StreamReader(file.OpenReadStream());
    return BitConverter
      .ToString(await md5.ComputeHashAsync(streamReader.BaseStream, cancellationToken: ct))
      .Replace("-", "");
  }
}