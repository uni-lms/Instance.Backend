using FastEndpoints;
using FluentValidation;

namespace Uni.Backend.Modules.Auth.Contracts;

public class LoginRequestValidator: Validator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(e => e.Email)
            .EmailAddress()
            .WithMessage("Email address field must be valid email address");

        RuleFor(e => e.Password)
            .NotEmpty()
            .WithMessage("Password must not be empty");
    }
}