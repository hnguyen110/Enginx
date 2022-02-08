using API.Handlers.Vehicle;
using API.Utilities.Constants;
using API.Utilities.Messages;
using FluentValidation;

namespace API.Validators.Vehicle;

public class UploadVehiclePictureValidator : AbstractValidator<UploadVehiclePicture.Command>
{
    public UploadVehiclePictureValidator()
    {
        RuleFor(e => e.File)
            .NotNull()
            .WithMessage(ValidationErrorMessages.Required)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.Required);

        RuleForEach(e => e.File)
            .Must(e =>
            {
                var extensions = new[] {"image/jpg", "image/jpeg", "image/png"};
                return extensions.Contains(e.ContentType);
            })
            .WithMessage(ValidationErrorMessages.UnsupportedFileFormat)
            .Must(e => e.Length <= AccountConstants.ProfilePictureSizeLimit)
            .WithMessage(ValidationErrorMessages.LargeFile);
    }
}