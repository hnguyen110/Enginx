using System.Net;
using API.Exceptions;
using API.Handlers.Account;
using API.Repositories.Address;
using API.Utilities.Messages;
using MediatR;

namespace API.Handlers.Address;

public class UpdateAddress
{
    public class Command : IRequest<Unit>
    {
        public string? Id { get; set; }
        public int StreetNumber { get; set; }
        public string? StreetName { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IAddressRepository _repository;
        
        public Handler(IAddressRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var record = await _repository
                .FindAddressById(
                    request.Id!, 
                    cancellationToken
                );
            
            if (record == null)
                throw new ApiException(
                    HttpStatusCode.NotFound, 
                    ApiErrorMessages.NotFound
                );

            record.StreetNumber = request.StreetNumber;
            record.StreetName = request.StreetName;
            record.City = request.City;
            record.State = request.State;
            record.Country = request.Country;
            record.PostalCode = request.PostalCode;

            await _repository.UpdateAddress(cancellationToken);
            return Unit.Value;
        }
    }
}