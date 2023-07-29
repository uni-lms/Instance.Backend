using FastEndpoints;

namespace Uni.Backend.Modules.Groups.Contracts;

public class GroupMapper: ResponseMapper<GroupDto, Group>
{
    public override GroupDto FromEntity(Group e) => new()
    {
        Name = e.Name,
        CurrentSemester = e.CurrentSemester,
        MaxSemester = e.MaxSemester
    };
}