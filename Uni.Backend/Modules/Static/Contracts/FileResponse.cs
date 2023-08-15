using JetBrains.Annotations;

namespace Uni.Backend.Modules.Static.Contracts;

public class FileResponse
{
    public required string FileId { [UsedImplicitly] get; set; }
    public required string Checksum { [UsedImplicitly] get; set; }
    public required string VisibleName { [UsedImplicitly] get; set; }
    
}