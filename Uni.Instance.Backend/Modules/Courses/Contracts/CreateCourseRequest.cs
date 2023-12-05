using JetBrains.Annotations;


namespace Uni.Backend.Modules.Courses.Contracts;

public class CreateCourseRequest {
  public required string Name { get; [UsedImplicitly] set; }
  public required string Abbreviation { get; [UsedImplicitly] set; }
  public required List<Guid> AssignedGroups { get; [UsedImplicitly] set; }
  public required List<Guid> Blocks { get; [UsedImplicitly] set; }
  public int Semester { get; [UsedImplicitly] set; }
}