namespace Backend.Modules.Static.Contracts;

public class UploadFileResponse
{
    public required string FileId { get; set; }
    public required string Checksum { get; set; }
    public required string VisibleName { get; set; }
    
}