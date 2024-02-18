using Aip.Instance.Backend.Data.Models;


namespace Aip.Instance.Backend.Api.Sections.Data;

public class GetAllCourseSectionsResponse {
  public required List<Section> Sections { get; set; }
}