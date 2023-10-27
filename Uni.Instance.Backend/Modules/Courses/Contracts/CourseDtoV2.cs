using JetBrains.Annotations;

using Uni.Backend.Modules.Groups.Contracts;
using Uni.Backend.Modules.Users.Contracts;


namespace Uni.Instance.Backend.Modules.Courses.Contracts;

public class CourseDtoV2 {
  public Guid Id { [UsedImplicitly] get; set; }
  public required string Name { [UsedImplicitly] get; set; }
  public required string Abbreviation { [UsedImplicitly] get; set; }

  public required float Progress { [UsedImplicitly] get; set; }
  public int Semester { [UsedImplicitly] get; set; }
  public List<string>? Tutors { [UsedImplicitly] get; set; }
}