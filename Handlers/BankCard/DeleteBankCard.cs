using System.Net;
using API.Exceptions;
using API.Repositories.BankCard;
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
        private readonly IBankCardRepository _repository;
        public Handler(IBankCardRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(Query request, CancellationToken cancellationToken)
        {
            var record = await _repository
                .RetrieveBankCardById(
                    request.Id, 
                    cancellationToken
                );
            
            if(record == null)
                throw new ApiException(
                    HttpStatusCode.NotFound,
                    ApiErrorMessages.NotFound
                );
                
            await _repository.DeleteBankCard(record, cancellationToken);
            return Unit.Value;
        }
    }
}