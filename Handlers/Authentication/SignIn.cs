using System.Net;
using API.DatabaseContext;
using API.Exceptions;
using API.Utilities.JWT.AccessToken;
using API.Utilities.Messages;
using API.Utilities.Security;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Handlers.Authentication;

public class JwtToken
{
    public string? Token { get; set; }
}

public abstract class SignIn
{
    public class Command : IRequest<JwtToken>
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
                .WithMessage(ValidationErrorMessages.Required);
            
            RuleFor(e => e.Password)
                .NotNull()
                .WithMessage(ValidationErrorMessages.Required)
                .NotEmpty()
                .WithMessage(ValidationErrorMessages.Required);
        }
    }
    
    public class Handler : IRequestHandler<Command, JwtToken>
    {
        private readonly Context _database;
        private readonly ISecurity _security;
        private readonly IAccessToken _jwt;

        public Handler(Context database, ISecurity security, IAccessToken jwt)
        {
            _database = database;
            _security = security;
            _jwt = jwt;
        }

        public async Task<JwtToken> Handle(Command request, CancellationToken cancellationToken)
        {
            var record = await _database
                .Credential
                !.FirstOrDefaultAsync(e => e.Username == request.Username,
                    cancellationToken);
            if (record == null) throw new Exception(ApiErrorMessages.NotFound);
            var isAuthenticated = _security
                .Compare(request.Password, record.PasswordSalt, record.PasswordHash);
            if (!isAuthenticated)
                throw new ApiException(HttpStatusCode.Unauthorized, ApiErrorMessages.Unauthenticated);
            return new JwtToken
            {
                Token = _jwt.CreateAccessToken(record)
            };
        }
    }
}