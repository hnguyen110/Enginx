using API.DTOs.BankCard;
using API.Repositories.BankCard;
using API.Utilities.CredentialAccessor;
using AutoMapper;
using MediatR;

namespace API.Handlers.BankCard;

public class CreateBankCard
{
    public class Command : IRequest<RetrieveBankCardDTO>
    {
        public string? CardType { get; set; }
        public string? CardHolderName { get; set; }
        public string? CardNumber { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string? CardVerificationCode { get; set; }
    }

    public class Handler : IRequestHandler<Command, RetrieveBankCardDTO>
    {
        private readonly ICredentialAccessor _accessor;
        private readonly IMapper _mapper;
        private readonly IBankCardRepository _repository;

        public Handler(IBankCardRepository repository, ICredentialAccessor accessor, IMapper mapper)
        {
            _repository = repository;
            _accessor = accessor;
            _mapper = mapper;
        }

        public async Task<RetrieveBankCardDTO> Handle(Command request, CancellationToken cancellationToken)
        {
            var card = new Models.BankCard
            {
                Id = Guid.NewGuid().ToString(),
                Account = _accessor.RetrieveAccountId(),
                CardType = request.CardType,
                CardHolderName = request.CardHolderName,
                CardNumber = request.CardNumber,
                ExpireDate = request.ExpireDate,
                CardVerificationCode = request.CardVerificationCode
            };

            await _repository.Save(card, cancellationToken);
            return _mapper.Map<Models.BankCard, RetrieveBankCardDTO>(card);
        }
    }
}