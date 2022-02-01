using API.DTOs.BankCard;
using API.Repositories.BankCard;
using API.Utilities.CredentialAccessor;
using AutoMapper;
using MediatR;

namespace API.Handlers.BankCard;

public class RetrieveAllBankCards
{
    public class Query : IRequest<List<RetrieveAllBankCardsDTO>>
    {
    }

    public class Handler : IRequestHandler<Query, List<RetrieveAllBankCardsDTO>>
    {
        private readonly ICredentialAccessor _accessor;
        private readonly IBankCardRepository _repository;
        private readonly IMapper _mapper;

        public Handler(ICredentialAccessor accessor, IBankCardRepository repository, IMapper mapper)
        {
            _accessor = accessor;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<RetrieveAllBankCardsDTO>> Handle(Query request, CancellationToken cancellationToken)
        {
            var id = _accessor.RetrieveAccountId();
            var records = await _repository
                .RetrieveAllBankCardsByAccount(
                    id,
                    cancellationToken
                );
            return _mapper
                .Map<List<Models.BankCard>, List<RetrieveAllBankCardsDTO>>(records);
        }
    }
}