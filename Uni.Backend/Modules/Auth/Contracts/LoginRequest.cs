﻿namespace Uni.Backend.Modules.Auth.Contracts;

public class LoginRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}