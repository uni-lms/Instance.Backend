using FastEndpoints;

using Riok.Mapperly.Abstractions;


namespace Uni.Backend.Modules.Users.Contracts;

[Mapper]
public partial class UserMapper : ResponseMapper<UserDto, User> {
  [MapProperty(new[] { nameof(User.Role), nameof(User.Role.Name) }, new[] { nameof(UserDto.RoleName) })]
  public partial UserDto FromEntity(User e);
}