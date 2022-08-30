using System.Net;
using API.Exceptions;
using API.Repositories.BankCard;
using API.Utilities.CredentialAccessor;
using API.Utilities.Messages;
using MediatR;

namespace API.Handlers.BankCard;

public class UpdateBankCard
{
    public class Command : IRequest<Unit>
    {
        public string? Id { get; set; }
        public string? CardType { get; set; }
        public string? CardHolderName { get; set; }
        public string? CardNumber { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string? CardVerificationCode { get; set; }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly ICredentialAccessor _accessor;

        private readonly IBankCardRepository _repository;

        public Handler(ICredentialAccessor accessor, IBankCardRepository repository)
        {
            _accessor = accessor;
            _repository = repository;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var record = await _repository.RetrieveBankCardById(request.Id, cancellationToken);
            if (record == null)
                throw new ApiException(
                    HttpStatusCode.NotFound,
                    ApiErrorMessages.NotFound
                );

            var updates = new Models.BankCard
            {
                CardType = request.CardType,
                CardHolderName = request.CardHolderName,
                CardNumber = request.CardNumber,
                ExpireDate = request.ExpireDate,
                CardVerificationCode = request.CardVerificationCode
            };

            await _repository
                .UpdateBankCard(record, updates, cancellationToken);

            return Unit.Value;
        }
    }
}