﻿namespace Uni.Backend.Modules.Auth.Contracts;

public class LoginResponse
{
    public required string Email { get; set; }
    public required string Token { get; set; }
}