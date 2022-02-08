using API.Handlers.Transaction;
using API.Utilities.Messages;
using FluentValidation;

namespace API.Validators.Transaction;

public class CreateTransactionValidator : AbstractValidator<CreateTransaction.Command>
{
    public CreateTransactionValidator()
    {
        RuleFor(e => e.Amount)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required)
            .Must(e => e > 0)
            .WithMessage(ValidationErrorMessages.InvalidTransactionAmount);

        RuleFor(e => e.Receiver)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required);
    }
}