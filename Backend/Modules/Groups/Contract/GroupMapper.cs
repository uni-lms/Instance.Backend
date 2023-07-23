using FastEndpoints;

namespace Backend.Modules.Groups.Contract;

public class GroupMapper: ResponseMapper<GroupDto, Group>
{
    public override GroupDto FromEntity(Group e) => new()
    {
        Name = e.Name,
        CurrentSemester = e.CurrentSemester,
        MaxSemester = e.MaxSemester
    };
}