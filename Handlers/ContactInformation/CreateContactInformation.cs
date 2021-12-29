using System.Net;
using API.DatabaseContext;
using API.Exceptions;
using API.Utilities.Messages;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Handlers.ContactInformation;

public class CreateContactInformation
{
    public class Command : IRequest<string>
    {
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? ContactNumber { get; set; }
    }

    public abstract class CommandValidator : AbstractValidator<Command>
    {
        protected CommandValidator()
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
        }
    }

    public class Handler : IRequestHandler<Command, string>
    {
        private readonly Context _database;

        public Handler(Context database)
        {
            _database = database;
        }

        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
            var record = await _database
                .ContactInformation
                !.FirstOrDefaultAsync(e =>
                        e.Email == request.Email
                        || e.ContactNumber == request.ContactNumber,
                    cancellationToken);

            if (request != null)
                throw new ApiException(HttpStatusCode.BadRequest, ApiErrorMessages.RecordExisted);

            var contact = new Models.ContactInformation
            {
                Id = new Guid().ToString(),
                FirstName = request!.FirstName,
                LastName = request!.LastName,
                MiddleName = request.MiddleName,
                Email = request!.Email,
                ContactNumber = request!.ContactNumber
            };

            await _database.AddAsync(contact, cancellationToken);
            await _database.SaveChangesAsync(cancellationToken);
            return contact.Id;
        }
    }
}