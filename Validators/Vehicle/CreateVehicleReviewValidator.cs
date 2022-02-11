using API.Handlers.Vehicle;
using API.Utilities.Messages;
using FluentValidation;

namespace API.Validators.Vehicle;

public class CreateVehicleReviewValidator : AbstractValidator<CreateReview.Command>
{
    public CreateVehicleReviewValidator()
    {
        RuleFor(e => e.Description)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required);
    }
}