using Uni.Instance.Backend.Data.Models;


namespace Uni.Instance.Backend.Endpoints.CourseSections.Data;

public class GetAllCourseSectionsResponse {
  public required List<CourseSection> Sections { get; set; }
}