using FastEndpoints;

using Riok.Mapperly.Abstractions;


namespace Uni.Backend.Modules.Users.Contracts;

public partial class UserMapper : ResponseMapper<UserDto, User> {
  public UserDto FromEntity(User e) {
    var dto = new UserDto {
      Id = e.Id,
      FirstName = e.FirstName,
      LastName = e.LastName,
      Patronymic = e.Patronymic,
      RoleName = e.Role?.Name,
    };

    return dto;
  }
}