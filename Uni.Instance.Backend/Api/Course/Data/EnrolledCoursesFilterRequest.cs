using System.Text.Json.Serialization;


namespace Uni.Instance.Backend.Api.Course.Data;

public class EnrolledCoursesFilterRequest {
  [JsonConverter(typeof(JsonStringEnumConverter))]
  public required CourseTypes CourseType { get; set; }
}