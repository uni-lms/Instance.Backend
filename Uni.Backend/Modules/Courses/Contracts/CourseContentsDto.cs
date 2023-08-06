namespace Uni.Backend.Modules.Courses.Contracts;

public class CourseContentsDto
{
    public required string Name { get; set; }
    public int Semester { get; set; }
    public required List<string> Owners { get; set; }
    
}