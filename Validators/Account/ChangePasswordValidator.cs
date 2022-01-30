using API.Handlers.Account;
using API.Utilities.Messages;
using FluentValidation;

namespace API.Validators.Account;

public class ChangePasswordValidator : AbstractValidator<ChangePassword.Command>
{
    public ChangePasswordValidator()
    {
        RuleFor(e => e.OldPassword)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required);
        
        RuleFor(e => e.NewPassword)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required);
    }
}