using System.Text.Json.Serialization;


namespace Aip.Instance.Backend.Data.Models;

public class StaticFile {
  public required string Id { get; set; }
  public required string Checksum { get; set; }
  public required string Filename { get; set; }

  [JsonIgnore]
  public string? Filepath { get; set; }
}