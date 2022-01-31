using System.Data;
using API.Handlers.Vehicle;
using API.Utilities.Messages;
using FluentValidation;

namespace API.Validators.Vehicle;

public class CreateVehicleValidator : AbstractValidator<CreateVehicle.Command>
{
    public CreateVehicleValidator()
    {
        RuleFor(e => e.Location)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required);

        RuleFor(e => e.Make)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required);

        RuleFor(e => e.Model)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required);

        RuleFor(e => e.BodyType)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required);

        RuleFor(e => e.EngineType)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required);

        RuleFor(e => e.TransmissionType)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required);

        RuleFor(e => e.FuelType)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required);

        RuleFor(e => e.Mileage)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required);

        RuleFor(e => e.NumOfSeats)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required);

        RuleFor(e => e.MaxNumOfSeats)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required);

        RuleFor(e => e.RentalPrice)
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