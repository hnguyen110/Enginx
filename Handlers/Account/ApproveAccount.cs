using System.Net;
using API.Exceptions;
using API.Repositories.Account;
using API.Utilities.Messages;
using API.Utilities.Security;
using MediatR;

namespace API.Handlers.Account;

public class ApproveAccount
{
    public class Command : IRequest<Unit>
    {
        public string? Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IAccountRepository _account;
        private readonly IAuthorization _authorization;

        public Handler(IAuthorization authorization, IAccountRepository account)
        {
            _authorization = authorization;
            _account = account;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var isAdministrator = await _authorization.IsAdministrator();
            if (!isAdministrator)
                throw new ApiException(
                    HttpStatusCode.Unauthorized,
                    ApiErrorMessages.Unauthorized
                );

            var record = await _account.FindById(request.Id!, cancellationToken);
            if (record == null)
                throw new ApiException(
                    HttpStatusCode.NotFound,
                    ApiErrorMessages.NotFound
                );

            await _account.ApproveAccount(record, cancellationToken);
            return Unit.Value;
        }
    }
}