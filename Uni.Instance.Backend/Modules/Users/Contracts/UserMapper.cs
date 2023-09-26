using FastEndpoints;

using Riok.Mapperly.Abstractions;


namespace Uni.Backend.Modules.Users.Contracts;

[Mapper]
public partial class UserMapper : ResponseMapper<UserDto, User> {
  [MapProperty(nameof(User.Role), nameof(UserDto.RoleName))]
  [MapProperty(nameof(User.Gender), nameof(UserDto.GenderName))]
  public partial UserDto FromEntity(User e);
}