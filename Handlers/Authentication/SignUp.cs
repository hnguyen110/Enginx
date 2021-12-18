using System.Net;
using API.DatabaseContext;
using API.Exceptions;
using API.Models;
using API.Utilities.Messages;
using API.Utilities.Security;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Handlers.Authentication;

public class SignUp
{
    public class Command : IRequest<Unit>
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

    public abstract class CommandValidator : AbstractValidator<Command>
    {
        protected CommandValidator()
        {
            RuleFor(e => e.Username)
                .NotNull()
                .WithMessage(ValidationErrorMessages.Required)
                .NotEmpty()
                .WithMessage(ValidationErrorMessages.Required)
                .MinimumLength(10)
                .WithMessage(ValidationErrorMessages.MinimumLength);
            
            RuleFor(e => e.Password)
                .NotNull()
                .WithMessage(ValidationErrorMessages.Required)
                .NotEmpty()
                .WithMessage(ValidationErrorMessages.Required)
                .MinimumLength(10)
                .WithMessage(ValidationErrorMessages.MinimumLength)
                .MaximumLength(15)
                .WithMessage(ValidationErrorMessages.MaximumLength);
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
                .Credential
                !.FirstOrDefaultAsync(e => e.Username == request.Username, cancellationToken);
            if (record != null)
                throw new ApiException(HttpStatusCode.BadRequest, ApiErrorMessages.RecordExisted);
            var salt = _security.CreatePasswordSalt();
            var hash = _security.CreatePasswordHash(request.Password, salt);
            var account = new Credential
            {
                Username = request.Username,
                PasswordSalt = salt,
                PasswordHash = hash
            };
            await _database.AddAsync(account, cancellationToken);
            await _database.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}