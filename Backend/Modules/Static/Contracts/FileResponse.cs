namespace Backend.Modules.Static.Contracts;

public class FileResponse
{
    public required string FileId { get; set; }
    public required string Checksum { get; set; }
    public required string VisibleName { get; set; }
    
}