using System.Net;
using API.Exceptions;
using API.Repositories.BankCard;
using API.Utilities.CredentialAccessor;
using API.Utilities.Messages;
using MediatR;

namespace API.Handlers.BankCard;

public class DeleteBankCard
{
    public class Query : IRequest<Unit>
    {
        public string? Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Unit>
    {
        private readonly ICredentialAccessor _accessor;
        private readonly IBankCardRepository _repository;

        public Handler(ICredentialAccessor accessor, IBankCardRepository repository)
        {
            _accessor = accessor;
            _repository = repository;
        }

        public async Task<Unit> Handle(Query request, CancellationToken cancellationToken)
        {
            var record = await _repository
                .RetrieveBankCardByAccount(
                    _accessor.RetrieveAccountId(),
                    request.Id,
                    cancellationToken
                );
            if (record == null)
                throw new ApiException(
                    HttpStatusCode.NotFound,
                    ApiErrorMessages.NotFound
                );

            await _repository.DeleteBankCard(record, cancellationToken);
            return Unit.Value;
        }
    }
}