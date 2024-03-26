using System.Text.Json;
using System.Text.Json.Serialization;

using Aip.Instance.Backend.Api.Content.Common.Data;


namespace Aip.Instance.Backend.Configuration.Serializators;

public class ContentConverter : JsonConverter<IContentItem> {
  public override IContentItem? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
    var jsonDoc = JsonDocument.ParseValue(ref reader);
    var root = jsonDoc.RootElement;

    var type = root.GetProperty("Type").GetString();
    return type switch {
      "text"       => JsonSerializer.Deserialize<TextContentItem>(root.GetRawText(), options),
      "file"       => JsonSerializer.Deserialize<FileContentItem>(root.GetRawText(), options),
      "link"       => JsonSerializer.Deserialize<LinkContentItem>(root.GetRawText(), options),
      "assignment" => JsonSerializer.Deserialize<AssignmentContentItem>(root.GetRawText(), options),
      _            => throw new JsonException($"Unknown type {type}"),
    };
  }

  public override void Write(Utf8JsonWriter writer, IContentItem value, JsonSerializerOptions options) {
    JsonSerializer.Serialize(writer, value, value.GetType(), options);
  }
}