using FastEndpoints;
using FluentValidation;

namespace Uni.Backend.Modules.Static.Contracts;

public class UploadFileRequestValidator: Validator<UploadFileRequest>
{
    public UploadFileRequestValidator()
    {
        RuleFor(e => e.VisibleName)
            .NotEmpty()
            .WithMessage("Visible name of file must not be empty");
    }
}