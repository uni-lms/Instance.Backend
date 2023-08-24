using FastEndpoints;

using Riok.Mapperly.Abstractions;


namespace Uni.Backend.Modules.Groups.Contracts;

[Mapper]
public partial class GroupMapper : ResponseMapper<GroupDto, Group> {
  public partial GroupDto FromEntity(Group e);
}