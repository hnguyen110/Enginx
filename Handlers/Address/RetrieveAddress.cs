using System.Net;
using API.Exceptions;
using API.Repositories.Address;
using API.Utilities.Messages;
using MediatR;

namespace API.Handlers.Address;

public class RetrieveAddress
{
    public class Query : IRequest<Models.Address>
    {
        public string? Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Models.Address>
    {
        private readonly IAddressRepository _repository;

        public Handler(IAddressRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<Models.Address> Handle(Query request, CancellationToken cancellationToken)
        {
            var record = await _repository.FindAddressById(request.Id!, cancellationToken);
            if (record == null)
                throw new ApiException(HttpStatusCode.NotFound, ApiErrorMessages.NotFound);
            return record;
        }
    }
}