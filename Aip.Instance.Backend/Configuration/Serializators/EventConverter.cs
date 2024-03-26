using System.Text.Json;
using System.Text.Json.Serialization;

using Aip.Instance.Backend.Api.Calendar.Data;


namespace Aip.Instance.Backend.Configuration.Serializators;

public class EventConverter : JsonConverter<IEvent> {
  public override IEvent? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
    var jsonDoc = JsonDocument.ParseValue(ref reader);
    var root = jsonDoc.RootElement;

    var type = root.GetProperty("Type").GetString();
    return type switch {
      "deadline" => JsonSerializer.Deserialize<DeadlineEvent>(root.GetRawText(), options),
      _          => throw new JsonException($"Unknown type {type}"),
    };
  }

  public override void Write(Utf8JsonWriter writer, IEvent value, JsonSerializerOptions options) {
    JsonSerializer.Serialize(writer, value, value.GetType(), options);
  }
}