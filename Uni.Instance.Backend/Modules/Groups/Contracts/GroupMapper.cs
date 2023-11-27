using FastEndpoints;

using Riok.Mapperly.Abstractions;


namespace Uni.Backend.Modules.Groups.Contracts;

[Mapper]
public partial class GroupMapper : ResponseMapper<GroupDto, Group> {
  public GroupDto FromEntity(Group e) {
    var dto = new GroupDto {
      Name = e.Name,
      CurrentSemester = e.CurrentSemester,
      MaxSemester = e.MaxSemester,
    };

    return dto;
  }
}