using System.Net;
using API.DatabaseContext;
using API.Exceptions;
using API.Models;
using API.Repositories.BankCard;
using API.Utilities.CredentialAccessor;
using API.Utilities.Messages;
using MediatR;

namespace API.Handlers.BankCard;

public class CreateBankCard
{
    public class Command : IRequest<Models.BankCard>
    {
        public string? CardType { get; set; }
        public string? CardHolderName { get; set; }
        public string? CardNumber { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string? CardVerificationCode { get; set; }
    }

    public class Handler : IRequestHandler<Command, Models.BankCard>
    {
        private readonly Context _context;
        private readonly IBankCardRepository _repository;
        private readonly ICredentialAccessor _accessor;

        public Handler(Context context, IBankCardRepository repository, ICredentialAccessor accessor)
        {
            _context = context;
            _repository = repository;
            _accessor = accessor;
        }
        
        public async Task<Models.BankCard> Handle(Command request, CancellationToken cancellationToken)
        {
            var username = _accessor.RetrieveAccountName();
            var account = _context.Account!.FirstOrDefault(e=> e.Id == _accessor.RetrieveAccountId().ToString());
            var bankCard = new Models.BankCard
            {
                Id = Guid.NewGuid().ToString(),
                CardType = request.CardType,
                CardHolderName = request.CardHolderName,
                CardNumber = request.CardNumber,
                ExpireDate = request.ExpireDate,
                CardVerificationCode = request.CardVerificationCode
            };
            
            if (account!.Role == Role.Administrator)
                throw new ApiException(HttpStatusCode.BadRequest, ApiErrorMessages.Unauthorized);
            await _repository.Save(bankCard, cancellationToken);
            return bankCard;
        }
    }
}