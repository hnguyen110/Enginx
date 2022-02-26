using System.Net;
using API.Exceptions;
using API.Repositories.BankCard;
using API.Utilities.Messages;
using API.Utilities.Security;
using MediatR;
using Microsoft.VisualBasic;

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
        private readonly IAuthorization _authorization;
        public Handler(IBankCardRepository repository, IAuthorization authorization)
        {
            _repository = repository;
            _authorization = authorization;
        }

        public async Task<Unit> Handle(Query request, CancellationToken cancellationToken)
        {
            var isCustomer = await _authorization.IsCustomer();
            
            if(!isCustomer)
                throw new ApiException(
                    HttpStatusCode.Unauthorized,
                    ApiErrorMessages.Unauthorized
                );
            
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