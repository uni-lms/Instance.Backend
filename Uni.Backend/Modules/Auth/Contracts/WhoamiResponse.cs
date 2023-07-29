﻿using System.Security.Claims;

namespace Uni.Backend.Modules.Auth.Contracts;

public class WhoamiResponse
{
    public required string Email { get; set; }
    public required string Role { get; set; }
}