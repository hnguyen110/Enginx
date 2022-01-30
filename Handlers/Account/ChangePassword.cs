using API.Repositories.Account;
using API.Utilities.CredentialAccessor;
using MediatR;

namespace API.Handlers.Account;

public class ChangePassword
{
    public class Command : IRequest<Unit>
    {
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IAccountRepository _repository;
        private readonly ICredentialAccessor _accessor;

        public Handler(IAccountRepository repository, ICredentialAccessor accessor)
        {
            _repository = repository;
            _accessor = accessor;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var id = _accessor.RetrieveAccountId();
            await _repository
                .ChangePassword(
                    id,
                    request.OldPassword!,
                    request.NewPassword!,
                    cancellationToken
                );
            return Unit.Value;
        }
    }
}