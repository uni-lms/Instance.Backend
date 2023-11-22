using System.Text.Json.Serialization;

using Uni.Instance.Backend.Modules.Courses.Contracts;


namespace Uni.Instance.Backend.Modules.Journal.Contracts;

public class JournalItem {
  public Guid Id { get; set; }
  public string Name { get; set; }

  [JsonConverter(typeof(JsonStringEnumConverter))]
  public CourseItemType Type { get; set; }
  
  public string Status { get; set; }
}