using System.Net;
using API.Exceptions;
using API.Repositories.Account;
using API.Utilities.Messages;
using API.Utilities.Security;
using MediatR;

namespace API.Handlers.Account;

public class CreateAccount
{
    public class Command : IRequest<Unit>
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Address { get; set; }
        public string? ContactInformation { get; set; }
        public string? License { get; set; }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IAccountRepository _repository;
        private readonly ISecurity _security;

        public Handler(IAccountRepository repository, ISecurity security)
        {
            _repository = repository;
            _security = security;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var record = await _repository.FindByUsername(request.Username!, cancellationToken);
            if (record != null)
                throw new ApiException(HttpStatusCode.BadRequest, ApiErrorMessages.RecordExisted);

            var salt = _security.CreatePasswordSalt();
            var hash = _security.CreatePasswordHash(request.Password, salt);
            var account = new Models.Account
            {
                Id = Guid.NewGuid().ToString(),
                Username = request.Username,
                PasswordSalt = salt,
                PasswordHash = hash,
                ProfilePicture = request.ProfilePicture,
                Address = request.Address,
                ContactInformation = request.ContactInformation,
                License = request.License
            };

            await _repository.Save(account, cancellationToken);
            return Unit.Value;
        }
    }
}