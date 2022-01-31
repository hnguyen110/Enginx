using API.Repositories.BankCard;
using API.Utilities.CredentialAccessor;
using MediatR;

namespace API.Handlers.BankCard;

public class RetrieveAllBankCards
{
    public class Query : IRequest<List<Models.BankCard>>
    {
    }

    public class Handler : IRequestHandler<Query, List<Models.BankCard>>
    {
        private readonly ICredentialAccessor _accessor;
        private readonly IBankCardRepository _repository;

        public Handler(ICredentialAccessor accessor, IBankCardRepository repository)
        {
            _accessor = accessor;
            _repository = repository;
        }

        public Task<List<Models.BankCard>> Handle(Query request, CancellationToken cancellationToken)
        {
            var id = _accessor.RetrieveAccountId();
            var records = _repository.RetrieveAllBankCardsByAccount(id, cancellationToken);
            return records;
        }
    }
}