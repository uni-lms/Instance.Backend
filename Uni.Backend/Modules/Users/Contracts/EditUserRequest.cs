using FastEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace Uni.Backend.Modules.Users.Contracts;

public class EditUserRequest
{
    [FromRoute]
    [BindFrom("id")]
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? Patronymic { get; set; }
    public Guid Gender { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public IFormFile? Avatar { get; set; }
}