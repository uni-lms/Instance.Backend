using System.Security.Cryptography;
using System.Text;
using Backend.Modules.Static.Contracts;

namespace Backend.Modules.Static.Services;

public class StaticService
{
    private readonly string _uploadsPath;

    public StaticService(IHostEnvironment hostEnvironment)
    {
        _uploadsPath = Path.Combine(hostEnvironment.ContentRootPath, "uploads");
    }

    public async Task<FileSaveResult> SaveFile(IFormFile file)
    {
        if (file.Length <= 0)
        {
            return new FileSaveResult
            {
                IsSuccess = false
            };
        }

        Directory.CreateDirectory(_uploadsPath);
        var fileId = ShortId.FromGuid(Guid.NewGuid());
        var fileName = new StringBuilder(fileId)
            .Append(Path.GetExtension(file.FileName))
            .ToString();
        var path = Path.Combine(_uploadsPath, fileName);
        await using var fileStream = new FileStream(path, FileMode.Create);
        await file.CopyToAsync(fileStream);

        return new FileSaveResult
        {
            IsSuccess = true,
            FileId = fileId,
            FilePath = path
        };
    }

    public async Task<string> GetChecksum(IFormFile file)
    {
        using var md5 = MD5.Create();
        using var streamReader = new StreamReader(file.OpenReadStream());
        return BitConverter.ToString(await md5.ComputeHashAsync(streamReader.BaseStream)).Replace("-", "");
    }
}