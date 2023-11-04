using System.Text.Json.Serialization;


namespace Uni.Instance.Backend.Modules.Courses.Contracts;

public class CourseItemDto {
  public Guid Id { get; set; }
  public required string VisibleName { get; set; }

  [JsonConverter(typeof(JsonStringEnumConverter))]
  public CourseItemType Type { get; set; }
}