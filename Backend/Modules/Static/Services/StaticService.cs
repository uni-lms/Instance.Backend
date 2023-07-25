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

    public async Task<string?> SaveFile(IFormFile file)
    {
        if (file.Length <= 0) return null;
        Directory.CreateDirectory(_uploadsPath);
        var fileName = new StringBuilder(ShortId.FromGuid(Guid.NewGuid()))
            .Append(Path.GetExtension(file.FileName))
            .ToString();
        var path = Path.Combine(_uploadsPath, fileName);
        await using var fileStream = new FileStream(path, FileMode.Create);
        await file.CopyToAsync(fileStream);

        return path;
    }

    public async Task<string> GetChecksum(IFormFile file)
    {
        using var md5 = MD5.Create();
        using var streamReader = new StreamReader(file.OpenReadStream());
        return BitConverter.ToString(await md5.ComputeHashAsync(streamReader.BaseStream)).Replace("-", "");
    }
}