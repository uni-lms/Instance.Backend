namespace Backend.Modules.Static.Contracts;

public class UploadFileRequest
{
    public required IFormFile File { get; set; }
    public required string VisibleName { get; set; }
}