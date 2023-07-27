using FastEndpoints;

namespace Backend.Modules.Static.Contracts;

public class SearchFileRequest
{
    public required string FileId { get; set; }
}