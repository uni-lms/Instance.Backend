using JetBrains.Annotations;


namespace Uni.Instance.Backend.Modules.Courses.Contracts; 

public class CourseTutorDto {
  public Guid Id { [UsedImplicitly] get; set; }
  public required string Name { [UsedImplicitly] get; set; }
  public required string Abbreviation { [UsedImplicitly] get; set; }

  public int Semester { [UsedImplicitly] get; set; }
  public List<string>? Groups { [UsedImplicitly] get; set; }
}