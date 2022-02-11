using API.Handlers.Insurance;
using API.Utilities.Messages;
using FluentValidation;

namespace API.Validators.Insurance;

public class CreateInsuranceValidator : AbstractValidator<CreateInsurance.Command>
{
    public CreateInsuranceValidator()
    {
        RuleFor(e => e.Name)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required);

        RuleFor(e => e.Price)
            .GreaterThan(0)
            .WithMessage(ValidationErrorMessages.InvalidInsuranceAmount)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required);

        RuleFor(e => e.Description)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required);
    }
}