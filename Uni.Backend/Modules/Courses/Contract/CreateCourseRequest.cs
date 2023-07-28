namespace Uni.Backend.Modules.Courses.Contract;

public class CreateCourseRequest
{
    public required string Name { get; set; }
    public required string Abbreviation { get; set; }
    public required IEnumerable<Guid> AssignedGroups { get; set; }
    public required IEnumerable<Guid> Blocks { get; set; }
    public required IEnumerable<Guid> Owners { get; set; }
    public int Semester { get; set; }
}