using System.Net;
using API.DTOs.Profile;
using API.Exceptions;
using API.Handlers.Account;
using API.Repositories.Address;
using API.Utilities.Messages;
using AutoMapper;
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
        private readonly IMapper _mapper;
        
        public Handler(IAddressRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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

            var address = new UpdateAddressDTO
            {
                City = request.City,
                Country = request.Country,
                PostalCode = request.PostalCode,
                State = request.State,
                StreetName = request.StreetName,
                StreetNumber = request.StreetNumber
            };
            
            _mapper.Map(address, record);
            
            await _repository.UpdateAddress(cancellationToken);
            return Unit.Value;
        }
    }
}