using API.Handlers.BankCard;
using API.Utilities.Messages;
using FluentValidation;

namespace API.Validators.BankCard;

public class CreateBankCardValidator : AbstractValidator<CreateBankCard.Command>
{
    public CreateBankCardValidator()
    {
        RuleFor(e => e.CardType)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required);

        RuleFor(e => e.CardHolderName)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required);

        RuleFor(e => e.CardNumber)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required)
            .Length(16)
            .WithMessage(ValidationErrorMessages.Length);

        RuleFor(e => e.ExpireDate)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required)
            .Must(date => date >= DateTime.Now)
            .WithMessage(ValidationErrorMessages.InvalidDateTime);

        RuleFor(e => e.CardVerificationCode)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required)
            .MinimumLength(3)
            .WithMessage(ValidationErrorMessages.MinimumLength)
            .MaximumLength(4)
            .WithMessage(ValidationErrorMessages.MaximumLength);
    }
}