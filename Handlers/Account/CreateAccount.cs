using System.Net;
using API.DatabaseContext;
using API.Exceptions;
using API.Utilities.Messages;
using API.Utilities.Security;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Handlers.Account;

public class CreateAccount
{
    public class Command : IRequest<Unit>
    {
        public string? Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Address { get; set; }
        public string? ContactInformation { get; set; }
        public string? License { get; set; }
    }

    public abstract class CommandValidator : AbstractValidator<Command>
    {
        protected CommandValidator()
        {
            RuleFor(e => e.Id)
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
            
            RuleFor(e => e.Address)
                .NotNull()
                .WithMessage(ValidationErrorMessages.Required)
                .NotEmpty()
                .WithMessage(ValidationErrorMessages.Required);
            
            RuleFor(e => e.ContactInformation)
                .NotNull()
                .WithMessage(ValidationErrorMessages.Required)
                .NotEmpty()
                .WithMessage(ValidationErrorMessages.Required);
            
            RuleFor(e => e.License)
                .NotNull()
                .WithMessage(ValidationErrorMessages.Required)
                .NotEmpty()
                .WithMessage(ValidationErrorMessages.Required);
        }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly Context _database;
        private readonly ISecurity _security;

        public Handler(Context database, ISecurity security)
        {
            _database = database;
            _security = security;
        }
        
        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var record = await _database
                .Account
                !.FirstOrDefaultAsync(e => e.Username == request.Username, cancellationToken);

            if (record != null)
                throw new ApiException(HttpStatusCode.BadRequest, ApiErrorMessages.RecordExisted);
            
            var salt = _security.CreatePasswordSalt();
            var hash = _security.CreatePasswordHash(request.Password, salt);
            var account = new Models.Account
            {
                Id = request.Id,
                Username = request.Username,
                PasswordSalt = salt,
                PasswordHash = hash,
                ProfilePicture = request.ProfilePicture,
                Address = request.Address
            };
            await _database.AddAsync(account, cancellationToken);
            await _database.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}