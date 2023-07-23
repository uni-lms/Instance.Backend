﻿namespace Backend.Modules.Users.Contract;

public class UserDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string RoleName { get; set; }
}