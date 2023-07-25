﻿using FastEndpoints;

namespace Backend.Modules.Users.Contract;

public class UserMapper: ResponseMapper<UserDto, User>
{
    public override UserDto FromEntity(User e) => new()
    {
        FirstName = e.FirstName,
        LastName = e.LastName,
        Patronymic = e.Patronymic,
        DateOfBirth = e.DateOfBirth,
        Email = e.Email,
        RoleName = e.Role.Name,
        GenderName = e.Gender.Name
    };
}