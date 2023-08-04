namespace Uni.Backend.Modules.Courses.Contract;

public class CreateCourseRequest
{
    public required string Name { get; set; }
    public required string Abbreviation { get; set; }
    public required List<Guid> AssignedGroups { get; set; }
    public required List<Guid> Blocks { get; set; }
    public required List<Guid> Owners { get; set; }
    public int Semester { get; set; }
}