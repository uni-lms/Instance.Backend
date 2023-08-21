using System.Text.Json.Serialization;


namespace Uni.Backend.Modules.Static.Contracts;

public class StaticFile {
  public required string Id { get; set; }

  [JsonIgnore]
  public string? Checksum { get; set; }

  public required string VisibleName { get; set; }

  [JsonIgnore]
  public string? FileName { get; set; }

  [JsonIgnore]
  public string? FilePath { get; set; }
}