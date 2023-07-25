using FastEndpoints;

namespace Backend.Modules.Static.Contracts;

public class DeleteFileRequest
{
    public string FileId { get; set; }
}