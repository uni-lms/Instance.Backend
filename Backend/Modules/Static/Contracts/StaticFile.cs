using Backend.Data;

namespace Backend.Modules.Static.Contracts;

public class StaticFile: BaseModel
{
    public required string Checksum { get; set; }
    public required string VisibleName { get; set; }
    public required string FileName { get; set; }
    public required string FilePath { get; set; }
}