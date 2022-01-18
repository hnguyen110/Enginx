using API.Handlers.Authentication;
using API.Utilities.Messages;
using FluentValidation;

namespace API.Validators.Authentication;

public class SignUpValidator : AbstractValidator<SignUp.Command>
{
    public SignUpValidator()
    {
        RuleFor(e => e.FirstName)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required);

        RuleFor(e => e.LastName)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required);

        RuleFor(e => e.Email)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required)
            .EmailAddress()
            .WithMessage(ValidationErrorMessages.EmailAddress);

        RuleFor(e => e.ContactNumber)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required);

        RuleFor(e => e.Username)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required);

        RuleFor(e => e.Password)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required);

        RuleFor(e => e.StreetName)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required);

        RuleFor(e => e.StreetNumber)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEqual(0)
            .WithMessage(ValidationErrorMessages.Required);

        RuleFor(e => e.City)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required);

        RuleFor(e => e.State)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required);

        RuleFor(e => e.Country)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required);

        RuleFor(e => e.PostalCode)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required);
    }
}