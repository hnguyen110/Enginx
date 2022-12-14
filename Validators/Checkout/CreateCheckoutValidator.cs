using API.Handlers.Checkout;
using API.Utilities.Messages;
using FluentValidation;

namespace API.Validators.Checkout;

public class CreateCheckoutValidator : AbstractValidator<CreateCheckout.Command>
{
    public CreateCheckoutValidator()
    {
        RuleFor(e => e.Vehicle)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required);

        RuleFor(e => e.BankCard)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required);

        RuleFor(e => e.CheckInDate)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required)
            .Must(e => e >= DateTime.Today)
            .WithMessage(ValidationErrorMessages.InvalidDateTime);

        RuleFor(e => e.CheckOutDate)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required)
            .Must(e => e >= DateTime.Today)
            .WithMessage(ValidationErrorMessages.InvalidDateTime);

        RuleFor(e => e.CheckInTime)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required);

        RuleFor(e => e.CheckOutDate)
            .GreaterThan(e => e.CheckInDate)
            .WithMessage(ValidationErrorMessages.InvalidCheckoutDate);

        RuleFor(e => e.CheckOutTime)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required);
    }
}