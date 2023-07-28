namespace Uni.Backend.Modules.Static.Contracts;

public class FileSaveResult
{
    public bool IsSuccess { get; set; }
    public string? FilePath { get; set; }
    public string? FileId { get; set; }
}