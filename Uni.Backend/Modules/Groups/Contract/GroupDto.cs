namespace Uni.Backend.Modules.Groups.Contract;

public class GroupDto
{
    public required string Name { get; set; }
    public int CurrentSemester { get; set; }
    public int MaxSemester { get; set; }
}