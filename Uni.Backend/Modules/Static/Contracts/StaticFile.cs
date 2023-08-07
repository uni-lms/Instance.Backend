using System.Text.Json.Serialization;
using Uni.Backend.Data;

namespace Uni.Backend.Modules.Static.Contracts;

public class StaticFile
{
    public required string Id { get; set; }
    [JsonIgnore] public required string Checksum { get; set; }
    public required string VisibleName { get; set; }
    [JsonIgnore] public required string FileName { get; set; }
    [JsonIgnore] public required string FilePath { get; set; }
}