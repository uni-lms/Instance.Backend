﻿using Uni.Backend.Modules.Users.Contracts;

namespace Uni.Backend.Modules.Groups.Contracts;

public class CreateGroupDto
{
    public required GroupDto Group { get; set; }
    public required IEnumerable<UserCredentials> UsersData { get; set; }
}